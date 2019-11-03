using MyOnlineStore.Interfaces.DataStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MyOnlineStore.DataStore
{
    public class DataStoreService<T> : IDataStoreService<T> where T : class
    {
      

        protected readonly HttpClient HttpClient;
        protected readonly Uri BaseAPIUri;
        protected Uri FullAPIUri { get; set; }
        public DataStoreService(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient("MyHttpClient");
            BaseAPIUri = new Uri($"{App.LocalBackendUrl}/{typeof(T).Name}/");
        }
       

        public bool AddItem(T item)
        {
            string uriString = $"/api/{typeof(T).Name}";

            var serializeObj = JsonConvert
                .SerializeObject(item,Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include});
        
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PostAsync(uriString, byteContent);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool DeleteItem(string id)
        {
            throw new NotImplementedException();
        }

        public T GetItem(string id)
        {
            var response = HttpClient.GetStringAsync($"/api/{nameof(T)}/{id}");
            T deserializeObject =  JsonConvert.DeserializeObject<T>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<T> GetAll(bool forceRefresh = false)
        {
            string uriString = $"/api/{typeof(T).Name}";
            var response = HttpClient.GetStringAsync(uriString);
            IEnumerable<T> deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<T>>(response.GetAwaiter().GetResult());
            return deserializeObjects;
        }

        public bool UpdateItem(T item, string id)
        {
            string uriString = $"/api/{typeof(T).Name}/{id}";

            var serializeObj = JsonConvert
             .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PutAsync(uriString, byteContent);

            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
