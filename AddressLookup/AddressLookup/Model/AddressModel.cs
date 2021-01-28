using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressLookup.Model
{
    public class AddressModel: INotifyPropertyChanged
    {
        private string _zipCode;
        public string ZipCode 
        {
            get 
            { 
                return _zipCode;
            }
            set
            {
                _zipCode = value;
                OnPropertyChanged("ZipCode");
            }
        }

        private string _address;
        public string Address 
        { 
            get
            {
                return _address;
            }                                                                                                                                              
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }
        
        public string Code { get; set; }

        public string Status { get; set; }

        public int AddressID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }




    }
}
