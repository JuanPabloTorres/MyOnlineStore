using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Utilities.Factories.ProductItemFactories
{
    public class ProductItemFactory : IProductItemFactory
    {
        public ProductItem CreateProductItem(ProductItem productItem)
        {
            return new ProductItem
            {
                Name = productItem.Name,
                Description = productItem.Description,
                Price = productItem.Price,
                Quantity = productItem.Quantity,
                Image = productItem.Image,
                Type = productItem.Type,
                MyStore = productItem.MyStore
            };
        }

        public ProductItem CreateProductItem(string itemName = null, string price = null, string description = null, string quantity = null, string type = null, byte[] logo = null, Store mystore = null)
        {
            return new ProductItem
            {
                Name = itemName,
                Description = description,
                Price = int.Parse(price),
                Quantity = int.Parse(quantity),
                Image = logo,
                Type = type,
                MyStore = mystore
            };
        }

        public ProductItem CreateSimpleProducItem()
        {
            return new ProductItem();
        }
    }
}
