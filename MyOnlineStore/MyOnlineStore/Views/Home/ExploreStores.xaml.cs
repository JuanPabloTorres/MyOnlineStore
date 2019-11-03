using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Shared.Models.Entries;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExploreStores : ContentPage
    {
        public ExploreStores()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IExploreStoresViewModel>();
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

        private void Filter_SearchButtonPressed(object sender, System.EventArgs e)
        {

        }
    }
}