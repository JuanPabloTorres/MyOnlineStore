using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Users;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace MyOnlineStore.DataStore
{
    public class UserDataStore<TUser> : DataStoreService<TUser>, IUserDataStore<TUser> where TUser : BaseUser
    {
        public UserDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            
        }

        public TUser GetUserByType(string email)
        {
            string urisString = $"/api/{typeof(TUser).Name}/{nameof(GetUserByType)}/{email}";
            var response = HttpClient.GetStringAsync(urisString);
            var deserializeObject = JsonConvert.DeserializeObject<TUser>(response.Result);

            return deserializeObject;
        }

        public TUser GetUserByEmail(string email)
        {
            string urisString = $"/api/{typeof(TUser).Name}/{nameof(GetUserByEmail)}/{email}";
            var response = HttpClient.GetStringAsync(urisString);
            var deserializeObject = JsonConvert.DeserializeObject<TUser>(response.Result);

            return deserializeObject;
        }

        public TUser GetByEmailWithAll(string email, Type type)
        {
            throw new NotImplementedException();
        }

        //public BaseUser GetByEmail(string email)
        //{
        //    string urisString = $"/api/{nameof(BaseUser)}/{nameof(GetByEmail)}/{email}";
        //    var response = HttpClient.GetStringAsync(urisString);
        //    var deserializeObject =  JsonConvert.DeserializeObject<BaseUser>(response.Result);          

        //    return deserializeObject;
        //}

        //public BaseUser GetByEmailWithAll(string email,Type type)
        //{
        //    string urisString = $"/api/{nameof(BaseUser)}/{nameof(GetByEmail)}/{email}";
        //    var response = HttpClient.GetStringAsync(urisString);
        //    var deserializeObject = JsonConvert.DeserializeObject<BaseUser>(response.Result);

        //    return deserializeObject;
        //}


    }
}
