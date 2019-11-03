using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.DataStore
{
    public class StoreDataStore : DataStoreService<Store>, IStoreDataStore
    {
        protected readonly HttpClient client;
        public StoreDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
        }

        public Store GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetClientFavorite(Guid clientId)
        {
            string urisString = $"/api/{typeof(Store).Name}/{nameof(GetClientFavorite)}/{clientId}";
            var response = HttpClient.GetStringAsync(urisString);
            IEnumerable<Store> stores = 
                JsonConvert.DeserializeObject<IEnumerable<Store>>(response.GetAwaiter().GetResult());
            return stores;
        }

        public bool StoreExists(string storeName, string storeOwnerName, Location location)
        {
            bool exists = false;
            string uriString = $"/api/{typeof(Store).Name}/{nameof(StoreExists)}/{storeOwnerName}/{storeName}";
            var serializeObj = JsonConvert
               .SerializeObject(location, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var request = new HttpRequestMessage(HttpMethod.Get, uriString);
            request.Content = byteContent;
            
            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(urisString));*/

            try
            {
                if (response.Result.IsSuccessStatusCode 
                    && response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("true"))
                {
                    exists = true;
                }
                else
                    exists = false;
            }
            catch (Exception)
            {
                exists = false;
            }
            

            return exists;
        }

        public ProductTest GetItemToAdd(string id)
        {

            var response = client.GetStringAsync("https://api.upcitemdb.com/prod/trial/lookup?upc=" + id);
            ProductTest deserializeObject = JsonConvert.DeserializeObject<ProductTest>(response.Result);
            return deserializeObject;

        }

        public Store GetStore(string storeName, string storeOwner, Location location )
        {
            string uriString = $"/api/{typeof(Store).Name}/{nameof(GetStore)}/{storeOwner}/{storeName}";
            Store store = null;
            var serializeObj = JsonConvert
               .SerializeObject(location, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Get, uriString);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request); /*Task.Run(async()=> await HttpClient.GetStringAsync(uriString));*/

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                store = JsonConvert.DeserializeObject<Store>(response.GetAwaiter().GetResult()
               .Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
           

            return store;
        }

        public ProductTest GetItemToAdd()
        {
            throw new NotImplementedException();
        }
    }
}
