using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.Validations
{
    public interface IValidateableObject<T>
    {
        List<string> Errors { get; }
        T Value { get; }
        List<IValidationRule<T>> ValidationsRules { get; }
        bool Validate();
    }
}
