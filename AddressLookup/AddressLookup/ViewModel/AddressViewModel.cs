using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressLookup.Model;
using AddressLookup.Command;

namespace AddressLookup.ViewModel
{
    public class AddressViewModel : INotifyPropertyChanged
    {

        public RelayCommand StartAddressLookup { get; set; }
        private AddressModel _addressModel;
        private string _result;

        public AddressViewModel()
        {
            _addressModel = new AddressModel();
            StartAddressLookup = new RelayCommand(Param=>AddressLookup(),param=>CanAddressLookup);
            _result = string.Empty;
        }


        #region Methods
        private void AddressLookup()
        {

            AddressRepository addressRepo = new AddressRepository();
            List<AddressModel> allAddresses = addressRepo.GetAllMatchingAddress(_addressModel.Address, _addressModel.ZipCode);
            if (allAddresses.Count == 0)
                Result = "\nNo Match Found.";
            else
            {
                if (allAddresses.Count > 0)
                    Result = "Multiple Match Found.";
                else
                    Result = "Single Address Found.";
                if (addressRepo.AddAddress(allAddresses))
                {
                    Result += "Address added successfully.";
                    if (addressRepo.UpdateAddress())
                        Result += "Single address status updated.";
                    else
                        Result += "Status is not updated for any address.";
                }
                else
                    Result += "Failure when adding address.";

            }

        }

        private bool CanAddressLookup
        {
            get
            {
                bool canLookup = true;




                return canLookup;
            }            
        }


        #endregion


        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        #region Model Object
        public AddressModel AddressModelObject
        {
            get
            {
                return _addressModel;
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler!=null)
               handler(this, new PropertyChangedEventArgs(propertyName));                
            
        }
    }
}
