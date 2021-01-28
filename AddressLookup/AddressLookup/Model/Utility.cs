using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressLookup.Model
{
    public static class Utility
    {

        public static string GetCodeFromCurrentAddress(string address, string zipCode)
        {
            string pathToExcelFile = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
             + "\\Sample_Address.xlsx";

            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
             + "\\Widne\\component\\help\\mypdf.pdf";

            ConnexionExcel ConxObject = new ConnexionExcel(pathToExcelFile);

            try
            {
                var query = (from p in ConxObject.UrlConnexion.Worksheet<CurrentAddress>("Sheet1")
                             where p.Address.Equals(address) && p.ZipCode.Equals(zipCode)
                             select new
                             {
                                 p.Code
                             }).FirstOrDefault();


                if (query != null)
                    return query.Code.ToString();
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}
