using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IStoreDataStore : IDataStoreService<Store>
    {
        Store GetByName(string name);
        IEnumerable<Store> GetClientFavorite(Guid clientId);
        bool StoreExists(string storeName, string storeOwnerName, Location location);
        Store GetStore(string storeName, string storeOwnerName, Location location);
        ProductTest GetItemToAdd(string id);
    }
}
