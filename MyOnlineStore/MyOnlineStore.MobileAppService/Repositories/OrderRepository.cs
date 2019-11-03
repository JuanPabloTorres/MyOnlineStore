using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MyOnlineStore.MobileAppService.Repositories
{
   public  class OrderRepository:Repository<Order>, IOrderRepository
    {
        public OrderRepository(MyContext context):base(context)
        { 
        
        }      

     
        public IEnumerable<Order> GetOrdersByCompleted()
        {
            var result = _Context.Orders.Where(s => s.IsCompleted == true);

            return result;
        }

        public IEnumerable<Order> GetOrdersByDate(DateTime date)
        {
            var result = _Context.Orders.Where(s => s.OrderDate == date);

            return result;
        }

      
    }
}
