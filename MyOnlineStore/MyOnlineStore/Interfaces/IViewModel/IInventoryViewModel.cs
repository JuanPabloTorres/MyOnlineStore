using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyOnlineStore.Interfaces.IViewModel
{
    public interface IInventoryViewModel 
    {
        ObservableCollection<ProductItem> InventoryItems { get; }
        ICommand NavigateToAddProductItemCommand { get;}
        ObservableCollection<ProductItem> FetchData();
    }
}
