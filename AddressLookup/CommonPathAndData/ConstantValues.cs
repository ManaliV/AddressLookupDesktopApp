using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPathAndData
{
    public class ConstantValues
    {
        public const string ValidZipCodeRegularExpression = "^[0-9]{5}$|^[0-9]{5}-[0-9]{4}$";


        public const string RequiredFieldMessage = "Required Field";
        public const string InvalidZipCode       = "Invalid Zip Code";

        public const string ConnectionString     = "Server = localhost; Database=C_Sharp_db; Trusted_Connection=True";

    }
}
