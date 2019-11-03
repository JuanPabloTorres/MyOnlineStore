using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Utilities;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyOnlineStore.Interfaces.IViewModel
{
    public interface IExploreStoresViewModel
    {
        Command<CommandEventData> SearchCommand { get; }
        ObservableCollection<StoreDTO> StoreList { get; }
        void Search(CommandEventData searchText);
    }
}
