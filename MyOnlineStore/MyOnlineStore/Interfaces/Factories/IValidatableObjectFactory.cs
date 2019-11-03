using MyOnlineStore.Interfaces.Validations;
using MyOnlineStore.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.Factories
{
    public interface IValidatableObjectFactory
    {
        ValidatableObject<T> CreateSimpleValidatebleObject<T>();
        ValidatableObject<T> CreateSimpleValidatebleObject<T>(T value);
    }
}
