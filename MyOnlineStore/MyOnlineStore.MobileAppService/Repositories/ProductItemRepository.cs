using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class ProductItemRepository : Repository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(MyContext context) : base(context)
        {
        }
    }
}
