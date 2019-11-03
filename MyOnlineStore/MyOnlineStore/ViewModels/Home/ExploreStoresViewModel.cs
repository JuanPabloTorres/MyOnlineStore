using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MyOnlineStore.ViewModels.Home
{
    public class ExploreStoresViewModel : Base.BaseViewModel, IExploreStoresViewModel
    {
        public Command<CommandEventData> SearchCommand { get; set; }

        private  IEnumerable<StoreDTO> _availableStores;
        public IEnumerable<StoreDTO> AvailableStores
        {
            get { return _availableStores; }
            set { _availableStores = value; }
        }

        private  ObservableCollection<StoreDTO> _storeList;
        public ObservableCollection<StoreDTO> StoreList
        {
            get { return _storeList; }
            set { _storeList = value; OnPropertyChanged(nameof(StoreList)); }
        }
        public ExploreStoresViewModel()
        {
            SearchCommand = new Command<CommandEventData>(Search);
            AvailableStores = GetStores();
            StoreList = new ObservableCollection<StoreDTO>(AvailableStores);
        }

        public void Search(CommandEventData searchData)
        {
            string searchText = searchData.Parameter.ToString();

            if (string.IsNullOrEmpty(searchText))
            {
                StoreList = new ObservableCollection<StoreDTO>(AvailableStores) ;
            }
            else
            {
                StoreList = new ObservableCollection<StoreDTO>(AvailableStores
                    .Where(t => t.StoreName.Contains(searchText) || t.Category.Contains(searchText)));
            }
        }
        public ObservableCollection<StoreDTO> GetStores()
        {
            ObservableCollection<StoreDTO> stores = new ObservableCollection<StoreDTO>()
            {
                new StoreDTO()
                {
                    Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Store 2",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },
                new StoreDTO()
                {
                     Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Example Store",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },

                new StoreDTO()
                {
                    Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Example Store",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },new StoreDTO()
                {
                    Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Example Store",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },
                new StoreDTO()
                {
                     Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Example Store",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },

                new StoreDTO()
                {
                    Description =@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    StoreName ="Example Store",
                    Category ="Type1",
                    StoreOwner ="Jose",
                    IsFavorite = true
                },
            };
            return stores;
        }
    }
}
