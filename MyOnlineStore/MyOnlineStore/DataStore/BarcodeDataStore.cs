using MyOnlineStore.Shared.Models.Purchases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyOnlineStore.DataStore
{
    public class BarcodeDataStore
    {

        protected readonly HttpClient HttpClient;

        public BarcodeDataStore()
        {
            HttpClient = new HttpClient();
        }
        public ProductTest GetItem(string id)
        {
            var response = HttpClient.GetStringAsync("https://api.upcitemdb.com/prod/trial/lookup?upc=" +id);
            ProductTest deserializeObject = JsonConvert.DeserializeObject<ProductTest>(response.Result);

           
            return deserializeObject;
        }


    }
}
