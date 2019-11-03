using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Roles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.DataStore
{
    public class RoleDataStore : DataStoreService<Role>, IRoleDataStore
    {
        public RoleDataStore(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {

        }

        public RoleType GetByRoleName(string name)
        {
            var response = HttpClient.GetStringAsync($"/api/{nameof(RoleType)}/{name}");
            var deserializeObject = JsonConvert.DeserializeObject<RoleType>(response.Result);

            return deserializeObject;
        }
    }
}
