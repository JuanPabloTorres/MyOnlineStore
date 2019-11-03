using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Utilities.Factories.ValidatableObjectsFactories
{
    public class ValidatableObjectsFactory : IValidatableObjectFactory
    {
        public ValidatableObject<T> CreateSimpleValidatebleObject<T>()
        {
            return new ValidatableObject<T>();
        }
        public ValidatableObject<T> CreateSimpleValidatebleObject<T>(T value)
        {
            return new ValidatableObject<T>(value);
        }
    }
}
