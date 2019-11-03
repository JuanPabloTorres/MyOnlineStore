using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyOnlineStore.DataStore
{
    public class ProductItemDataStore : DataStoreService<ProductItem>, IProductItemDataStore
    {
        public ProductItemDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public IEnumerable<ProductItem> GetBestSaleInventoryOf(int productitems)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetBestSaleInventoryOf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetHighQty(int qty)
        {
            throw new NotImplementedException();
        }

        public int GetInventoryCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetProductByQty()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetProductItemsWithQty(int qty)
        {
            throw new NotImplementedException();
        }

        public double GetValueOfProductsByQty(IEnumerable<ProductItem> productItems)
        {
            throw new NotImplementedException();
        }

        public double GetValueOfProductsBySales(IEnumerable<ProductItem> toCheckValues)
        {
            throw new NotImplementedException();
        }

        public double GetValueOfTopProducts(IEnumerable<ProductItem> toCheckValues)
        {
            throw new NotImplementedException();
        }
    }
}
