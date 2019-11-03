using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyOnlineStore.Interfaces.IViewModel
{
    public interface IReportViewModel
    {
        ObservableCollection<ProductItem> InventoryItems { get; }
        ObservableCollection<Order> Orders { get; }


    }
}
