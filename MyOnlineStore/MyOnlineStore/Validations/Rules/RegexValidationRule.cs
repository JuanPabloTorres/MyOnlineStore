using MyOnlineStore.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyOnlineStore.Validations.Rules
{
    public class RegexValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set ; }
        public string RegexString { get; set; }
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            bool isValid = Regex.IsMatch(str, RegexString);
            return isValid;
        }
    }
}
