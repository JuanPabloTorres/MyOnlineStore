using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IProductItemDataStore : IDataStoreService<ProductItem>
    {

        IEnumerable<ProductItem> GetProductItemsWithQty(int qty);
        IEnumerable<ProductItem> GetBestSaleInventoryOf();
        IEnumerable<ProductItem> GetProductByQty();

        IEnumerable<ProductItem> GetHighQty(int qty);
      


        double GetValueOfProductsBySales(IEnumerable<ProductItem> toCheckValues);
        double GetValueOfProductsByQty(IEnumerable<ProductItem> productItems);
        int GetInventoryCount();
    }
}
