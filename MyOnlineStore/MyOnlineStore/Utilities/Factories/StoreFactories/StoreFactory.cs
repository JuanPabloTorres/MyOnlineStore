using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Utilities.Factories.StoreFactories
{
    public class StoreFactory : IStoreFactory
    {
        public Store CreateSimpleStore()
        {
            return new Store();
        }

        public Store CreateStore(string storeName, string ownerName, string description, Location location, string category, byte[] logo)
        {
            return new Store
            {
                StoreName = storeName,
                StoreOwnerName = ownerName,
                Description = description,
                Location = location,
                Category = category,
                Logo = logo
            };
        }
    }
}
