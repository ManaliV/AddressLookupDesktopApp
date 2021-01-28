using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressLookup.Model
{
    public class CurrentAddress
    {
        public int Code
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string ZipCode
        {
            get;
            set;
        }
    }
}
