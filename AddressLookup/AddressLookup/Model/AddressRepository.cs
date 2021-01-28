﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPathAndData;

namespace AddressLookup.Model
{
    public class AddressRepository
    {
        private SqlConnection con;
        
        private void connection()
        {
            string constr = ConstantValues.ConnectionString;
            con = new SqlConnection(constr);

        }
        
        public bool AddAddress(List<AddressModel> addressModels)
        {

            connection();
            con.Open();
            foreach (var addressModel in addressModels)
            {
                try
                {
                    string query = "INSERT INTO tbl_my_address VALUES(@Code,@Address,@ZipCode,@Status,@AddressID)";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@Code", addressModel.Code);
                    sqlCmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    sqlCmd.Parameters.AddWithValue("@ZipCode", addressModel.ZipCode);
                    sqlCmd.Parameters.AddWithValue("@Status", addressModel.Status);
                    sqlCmd.Parameters.AddWithValue("@AddressID", addressModel.AddressID);
                    int result = sqlCmd.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        con.Close();
                        return false;
                    }
                }
                catch
                {
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return true;

        }

        private List<AddressModel> GetMatchingAddressHelper(string address, string zipCode, string tableName)
        {
            connection();
            List<AddressModel> addressList = new List<AddressModel>();
            string code = Utility.GetCodeFromCurrentAddress(address, zipCode);

            DataTable dbMatchingAddress = new DataTable();
            con.Open();
            string query = "SELECT * FROM " + tableName + " Where Address like @Address And Zipcode=@ZipCode";
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);
            sqlDa.SelectCommand.Parameters.AddWithValue(@"Address", address + "%" ?? (object)DBNull.Value);
            sqlDa.SelectCommand.Parameters.AddWithValue(@"ZipCode", zipCode ?? (object)DBNull.Value);
            sqlDa.Fill(dbMatchingAddress);

            foreach (DataRow row in dbMatchingAddress.Rows)
            {
                AddressModel addressModel = new AddressModel();
                addressModel.Address = row["Address"].ToString();
                addressModel.ZipCode = row["Zipcode"].ToString();
                addressModel.Status = "N";
                addressModel.Code = code;
                addressModel.AddressID = Convert.ToInt32(row["Id"]);

                addressList.Add(addressModel);
            }

            con.Close();
            return addressList;

        }

        public List<AddressModel> GetAllMatchingAddress(string address, string zipCode)
        {
            string allAddressTable = "tbl_all_address";
            string subAddressTable = "tbl_sub_address";
            List<AddressModel> allAddressMatchingModels = GetMatchingAddressHelper(address, zipCode, allAddressTable);
            List<AddressModel> subAddressMatchingModels = GetMatchingAddressHelper(address, zipCode, subAddressTable);

            allAddressMatchingModels.AddRange(subAddressMatchingModels);

            return allAddressMatchingModels;

        }

        public bool UpdateAddress()
        {

            connection();
            con.Open();

            string query = "update tbl_my_address set Status =@Status where Address in ( select Address from tbl_my_address group by Address  having count(ZipCode) =1)";
            SqlCommand sqlCmd = new SqlCommand(query, con);
            sqlCmd.Parameters.AddWithValue("@Status", "Y");
            int result = sqlCmd.ExecuteNonQuery();
            con.Close();
            if (result >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAddress()
        {
            connection();
            con.Open();
            con.Close();
            return true;
        }
    }
}
