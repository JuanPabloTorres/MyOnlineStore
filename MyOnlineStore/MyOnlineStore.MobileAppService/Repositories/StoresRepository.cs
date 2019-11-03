using Microsoft.EntityFrameworkCore;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class StoresRepository : Repository<Store>, IStoresRepository
    {
        public StoresRepository(MyContext context) : base(context)
        {
        }

        public Store GetByName(string name)
        {
            var result = _Context.Stores
                .Where(s => s.StoreName.Equals(name))
                .FirstOrDefault();
            return result;
        }

        public IEnumerable<Store> GetClientFavorites(Guid clientId)
        {
            var clientFavorites = _Context.ClientsFavoritesStores
                .Where(fs => fs.UserId == clientId);
            var result = _Context.Stores
                .Where(s => s.Id == clientFavorites.Select(cf => cf.StoreId).FirstOrDefault())
                .AsEnumerable();
            return result;
        }

        public async Task<Store> GetStore(string name, string ownerName, Location location)
        {
          var result = await Task.Run(()=>_Context.Stores.Where(store => store.StoreName.Equals(name) && store.StoreOwnerName.Equals(ownerName) && store.Location.Latitude.Equals(location.Latitude) && store.Location.Longitude.Equals(location.Longitude)));
            return result.FirstOrDefault();
        }

        public bool StoreExists(string ownerName,string storeName,Location location)
        {
            bool exist = _Context.Stores.Include(store => store.Location)
                .Where(store => store.StoreName == storeName
                && store.StoreOwnerName == ownerName
                && store.Location.Latitude == location.Latitude
                && store.Location.Longitude == location.Longitude)
                .Count() == 0 ? false : true;
            return exist;  
        }
    }
}
