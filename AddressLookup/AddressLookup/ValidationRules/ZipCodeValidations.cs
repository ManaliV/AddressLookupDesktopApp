using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommonPathAndData;


namespace AddressLookup.ValidationRules
{
    class ZipCodeValidations : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    var regex = new Regex(ConstantValues.ValidZipCodeRegularExpression); //regex that matches disallowed text
                    var parsingOk = !regex.IsMatch(value.ToString());
                    if (parsingOk)
                    {
                        validationResult = new ValidationResult(false, ConstantValues.InvalidZipCode);
                    }
                   
                }
            }

            return validationResult;
        }
    }
  
}
