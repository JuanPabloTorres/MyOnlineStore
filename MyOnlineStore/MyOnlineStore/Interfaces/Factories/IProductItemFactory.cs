using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.Factories
{
    public interface IProductItemFactory
    {
        ProductItem CreateSimpleProducItem();
        ProductItem CreateProductItem(ProductItem productItem);
        ProductItem CreateProductItem(string itemName = null, string price = null, string description = null, string quantity = null, string type = null, byte[] logo = null, Store mystore = null);
    }
}
