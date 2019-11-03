using MyOnlineStore.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Validations.Rules
{
    public class CompareValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get ; set ; }
        public object CompareToString { get; set; }
        public bool Check(T value)
        {
            bool isEqual;

            if (CompareToString != null)
            {
                isEqual = CompareToString.Equals(value);
            }
            else
                isEqual = false;

            return isEqual;
        }
    }
}
