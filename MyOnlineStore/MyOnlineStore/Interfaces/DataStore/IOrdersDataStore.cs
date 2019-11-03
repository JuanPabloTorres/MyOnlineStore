using MyOnlineStore.Dashboard.DashBoardModel;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IOrdersDataStore:IDataStoreService<Order>
    {

        IEnumerable<Order> GetOrdersByDate(DateTime date);

        int GetByCompleted(DateTime date);

        IEnumerable<Order> GetOrdersFromDateToDate(DateTime from, DateTime to);

        double GetValueOfItemsPurchaseWithOrder(DateTime date);

        IEnumerable<Order> GetProgressFromDateTo(DateTime from, DateTime to);

        IEnumerable<Record> GetRecords(DateTime from, DateTime to);



    }
}
