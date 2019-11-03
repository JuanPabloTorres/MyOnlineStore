using MyOnlineStore.Utilities;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Interfaces.DataStore;

namespace MyOnlineStore.ViewModels
{
    public class HomeViewModel : BaseViewModel,IHomeViewModel
    {
        private readonly IStoreDataStore _StoreDataStore;

        public ICommand DetailCommand { get; set; }
        public Command<CommandEventData> NavigateToStoreDetailCommand { get; set; }
        public ObservableCollection<StoreDTO> Stores { get; set; }
        public ObservableCollection<StoreDTO> FavoriteStoreList { get; set; }


        //========================================================================
        //
        // TODO: Inject Store Data Store
        //
        //========================================================================
        public HomeViewModel(IStoreDataStore storeDataStore)
        {
            Stores = new ObservableCollection<StoreDTO>();
            FavoriteStoreList = new ObservableCollection<StoreDTO>();
            Stores = GetStore();
            FavoriteStoreList = Stores;
            NavigateToStoreDetailCommand = new Command<CommandEventData>(async(data)=>await NavigateToStoreDetail(data));
            _StoreDataStore = storeDataStore;   
        }
       
        public ObservableCollection<StoreDTO> GetStore()
        {
            ObservableCollection<StoreDTO> stores = new ObservableCollection<StoreDTO>()
            {
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

        public async Task NavigateToStoreDetail(CommandEventData commandEventData)
        {
            var x = commandEventData;
        }
   

    }
}
