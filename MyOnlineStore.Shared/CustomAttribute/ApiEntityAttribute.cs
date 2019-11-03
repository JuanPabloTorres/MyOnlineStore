using System;

namespace MyOnlineStore.Shared.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ApiGenericEntity : Attribute
    {
    }
}
