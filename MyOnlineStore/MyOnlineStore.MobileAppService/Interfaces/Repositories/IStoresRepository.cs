using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IStoresRepository : IRepository<Store>
    {
        Store GetByName(string name);
        IEnumerable<Store> GetClientFavorites(Guid clientId);
        Task<Store> GetStore(string name, string ownerName, Location location);
        bool StoreExists(string ownerName, string storeName, Location location);
    }
}
