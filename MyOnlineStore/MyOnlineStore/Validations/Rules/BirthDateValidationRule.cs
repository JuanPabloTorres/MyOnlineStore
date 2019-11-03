using MyOnlineStore.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Validations.Rules
{
    public class BirthDateValidationRule : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; set; }

        public bool Check(DateTime value)
        {
            bool isValid ;

            if (value.Date.Year > 1900)
            {
                if (value.Date.Year < DateTime.Today.AddYears(-10).Year)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    ValidationMessage = @"Too young for using the app";
                }
            }
            else
                isValid = false;

            return isValid;
        }
    }
}
