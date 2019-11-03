using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
