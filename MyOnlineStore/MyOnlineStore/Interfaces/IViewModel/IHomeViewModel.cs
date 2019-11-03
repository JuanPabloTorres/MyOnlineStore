using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.Interfaces.IViewModel
{
    public interface IHomeViewModel
    {
        Command<CommandEventData> NavigateToStoreDetailCommand { get; }
        ObservableCollection<StoreDTO> Stores { get; }
        ObservableCollection<StoreDTO> FavoriteStoreList { get; }
        ObservableCollection<StoreDTO> GetStore();
    }
}
