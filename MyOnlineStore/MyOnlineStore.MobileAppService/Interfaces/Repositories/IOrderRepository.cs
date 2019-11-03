using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order> 
    {
        IEnumerable<Order> GetOrdersByDate(DateTime date);
        IEnumerable<Order> GetOrdersByCompleted();
    }
}
