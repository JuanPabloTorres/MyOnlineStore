using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Purchases;

namespace MyOnlineStore.DataStore
{
    public class MockDataStoreOrder : MockDataStore<Order>,IOrdersDataStore
    {
        List<Order> orders;

        List<ProductItem> items;

        
        public MockDataStoreOrder()
        {
            orders = new List<Order>();

            items = new List<ProductItem>()
            {
                new ProductItem()
                {
                    Name="Item 1",
                     Description="this is item 1",
                      IsAvailable=true,
                       Price=12.50,
                       Type="Food"

                },
                 new ProductItem()
                {
                    Name="Item 2",
                     Description="this is item 2",
                     IsAvailable=true,
                       Price=12.50,
                       Type="Tool"

                },
                  new ProductItem()
                {
                    Name="Item 3",
                     Description="this is item 3",
                     IsAvailable=true,
                       Price=12.50,
                       Type="Furniture"

                },
            };

            var mockItems = new List<Order>
            {
                new Order { OrderId = Guid.NewGuid(),   OrderDate = DateTime.Today.AddDays(-1),  IsCompleted=false,  OrderItems=items, PayedWith="Credit Card" },

                      new Order { OrderId = Guid.NewGuid(),   OrderDate = DateTime.Today.AddMonths(-1),  IsCompleted=false,  OrderItems=items, PayedWith="Credit Card" },

                  new Order { OrderId = Guid.NewGuid(),   OrderDate = DateTime.Today.AddDays(-1),  IsCompleted=true,  OrderItems=items, PayedWith="Credit Card" },
                    new Order { OrderId = Guid.NewGuid(),   OrderDate = DateTime.Today.AddDays(-1),  IsCompleted=true,  OrderItems=items, PayedWith="Credit Card" },



            };

            foreach (var item in mockItems)
            {
                orders.Add(item);
            }

            genericList = orders;
        }

     

        public IEnumerable<Order> GetOrdersByDate(DateTime date)
        {
            return orders.Where(x => x.OrderDate == date);
        }

        public int GetByCompleted(DateTime date)
        {
           var completed= orders.Where(x => x.IsCompleted == true && x.OrderDate == date).ToList();

            return completed.Count();
        }

        public double GetValueOfItemsPurchaseWithOrderofDate(DateTime date)
        {
            return orders.Where(s=>s.OrderDate==date).Sum(x => x.OrderItems.Sum(z => z.Price * z.TotalPurchased));
        }

        public double GetValueOfItemsPurchaseWithOrder(DateTime date)
        {
            var selectedOrders= new List<Order>( orders.Where(s => s.OrderDate == date));

            double progress = selectedOrders.Sum(x => x.OrderItems.Sum(s => s.Price));

            return progress;
        }

        public IEnumerable<Record> GetRecords(DateTime from, DateTime to)
        {
            List<Record> records = new List<Record>();

            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                Record record = new Record();

                record.Orders= orders.Where(x => x.OrderDate == day && x.IsCompleted==true).ToList();

                record.RecordValue = record.Orders.Sum(x => x.OrderItems.Sum(s => s.Price));

                record.RecordDate = day;

                records.Add(record);



            }

            return records;

        }

        public IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to)
        {
            Record record = new Record();

            record.Orders = orders.Where(x => x.OrderDate >= from && x.OrderDate <= to).ToList();

            return record.Orders;

        }

        public IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to)
        {
            return orders.Where(x => x.OrderDate >= from && x.OrderDate <= to);
        }
    }
}