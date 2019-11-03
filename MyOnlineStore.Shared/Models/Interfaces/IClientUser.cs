using MyOnlineStore.Shared.Models.Stores;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Models.Interfaces
{
    public interface IClientUser 
    {
        ICollection<FavoriteStoreClient> FavoriteStores { get; }
    }
}
