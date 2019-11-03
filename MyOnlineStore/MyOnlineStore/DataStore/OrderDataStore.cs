using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.DataStore
{
    public class OrderDataStore : DataStoreService<Order>, IOrdersDataStore
    {
        protected readonly HttpClient client;

        public OrderDataStore(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
        }

        public int GetByCompleted(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByDate(DateTime date)
        {
            string uri = $"{  nameof(GetOrdersByDate) } / {date}";
            FullAPIUri = new Uri(BaseAPIUri,uri);

            var reponse = HttpClient.GetStringAsync(FullAPIUri);
            var deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<Order>>(reponse.Result);

            var lsit = new List<Order>(deserializeObjects);
            return deserializeObjects;

        }

        public IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Record> GetRecords(int days)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Record> GetRecords(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public double GetValueOfItemsPurchaseWithOrder(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
