using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOnlineStore.DataStore
{
    public class MockInventoryDataStore:MockDataStore<ProductItem>,IProductItemDataStore
    {
        List<ProductItem> items;

        public MockInventoryDataStore()
        {
           

            items = new List<ProductItem>()
            {
                new ProductItem()
                {
                  Name = "Item",
                     Description="This is item",
                     Price=12.00,
                      IsAvailable=true,
                       Quantity=5*10,
                       TotalPurchased=2*2,
                       Type="Food",
                },
                 new ProductItem()
                {
                  Name = "Item",
                     Description="This is item",
                     Price=12.00,
                      IsAvailable=true,
                       Quantity=5*10,
                       TotalPurchased=2*3,
                       Type="Food",
                },
                  new ProductItem()
                {
                  Name = "Item",
                     Description="This is item",
                     Price=12.00,
                      IsAvailable=true,
                       Quantity=5*5,
                       TotalPurchased=2*4,
                       Type="Food",
                },
                   new ProductItem()
                {
                  Name = "Item",
                     Description="This is item",
                     Price=12.00,
                      IsAvailable=true,
                       Quantity=5*2,
                       TotalPurchased=2*4,
                       Type="Car Tools",
                }
            };

            genericList = items;
        }

        public IEnumerable<ProductItem> GetBestSaleInventoryOf()
        {

            var topProducts = new List<ProductItem>(items.OrderByDescending(x => x.TotalPurchased));
          

            return topProducts;
        }

        public IEnumerable<ProductItem> GetHighQty(int qty)
        {
            return items.Where(x => x.Quantity >= qty);

        }

        public int GetInventoryCount()
        {
            return items.Count;
        }

        public IEnumerable<ProductItem> GetProductByQty()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetProductItemsWithQty(int qty)
        {
        return items.Where(x => x.Quantity <= qty);

          
        }

        public double GetValueOfProductsByQty(IEnumerable<ProductItem> productItems)
        {
            List<ProductItem> products = new List<ProductItem>(productItems);

             double totalValue = productItems.Sum(x => x.Price * x.Quantity);

            return totalValue;
        }



        public double GetValueOfProductsBySales(IEnumerable<ProductItem> toCheckValues)
        {
            List<ProductItem> products = new List<ProductItem>(toCheckValues);


            double value = products.Sum(x => x.Price * x.TotalPurchased);

            return value;

        }

       

        List<ProductItem> GetProductItems()
        {
            var items = new List<ProductItem>();

            for (int i = 0; i < 8; i++)
            {

                items.Add(new ProductItem()
                {
                    Name = "Name" + i,
                     Description="This is item"+i,
                     Price=12.00,
                      IsAvailable=true,
                       Quantity=5*i,
                       TotalPurchased=2*i,
                       Type="Food",
                });
            }
            return items;
        }
    }
}
