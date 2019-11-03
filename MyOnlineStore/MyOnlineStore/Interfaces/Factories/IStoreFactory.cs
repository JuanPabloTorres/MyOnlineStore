using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.Factories
{
    public interface IStoreFactory
    {
        Store CreateSimpleStore();
        Store CreateStore(string storeName, string ownerName, string description, Location location, string category,
            byte[] logo);
    }
}
