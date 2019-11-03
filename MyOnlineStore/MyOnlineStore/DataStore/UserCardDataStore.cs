using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Users;
using System.Net.Http;

namespace MyOnlineStore.DataStore
{
    public class UserCardDataStore : DataStoreService<UserCard>, IUserCardDataStore
    {
        public UserCardDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
