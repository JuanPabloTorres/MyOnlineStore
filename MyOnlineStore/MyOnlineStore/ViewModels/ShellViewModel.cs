using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.ViewInterfaces;
using MyOnlineStore.Shared.Models.Stores;
using MyOnlineStore.Utilities;
using MyOnlineStore.ViewModels.Base;
using MyOnlineStore.Views.AdminScenarios;
using MyOnlineStore.Views.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using MyOnlineStore.CustomControls;
using MyOnlineStore.Converters;
using MyOnlineStore.Views.Administrator;

namespace MyOnlineStore.ViewModels
{

    public class ShellViewModel :BaseViewModel, IShellViewModel
    {
        protected readonly IStoreDataStore _StoreDataStore;

        protected readonly Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }

        /// <summary>
        /// Starting get value with CurrentStoreKey. If null sets Empty
        /// </summary>
        private string currentStoreId = Preferences.Get(nameof(CurrentStoreId), string.Empty);
        public string CurrentStoreId
        {
            get => currentStoreId;
            set
            {
                currentStoreId = value;
                Preferences.Set(nameof(CurrentStoreId), value);
                //RaisePropertyChanged(nameof(CurrentStoreId));
            }
        }

        private static Store _CurrentStore;
        public Store CurrentStore
        {
            get => _CurrentStore;
            set
            {
                _CurrentStore = value;
                CurrentStoreId = CurrentStore.Id.ToString();
                RaisePropertyChanged(() => CurrentStore);
            }
        }

        private bool hasMoreThanOne = false;
        public bool HasMoreThanOneStore
        {
            get => hasMoreThanOne;
            set
            {
                hasMoreThanOne = value;
                RaisePropertyChanged(() => HasMoreThanOneStore);
            }
        }

        public ObservableCollection<Store> MyStores { get; set; }

        private View headerContent;
        public View HeaderContent
        {
            get => headerContent;
            set
            {
                headerContent = value;
                RaisePropertyChanged(() => HeaderContent);
            }
        }
        public ShellViewModel(IStoreDataStore storeDataStore)
        {
            _StoreDataStore = storeDataStore;

            Init();
        }

        /// <summary>
        /// Initialize ViewModel necesary methods
        /// </summary>
        private void Init()
        {
            MyStores = FetchStores();
            SetCurrentStore();
            RegisterRoutes();
        }

        /// <summary>
        /// Fetch <cref>CurrentUser</cref> Stores
        /// </summary>
        private ObservableCollection<Store> FetchStores()
        {

            Guid id;
            if (!string.IsNullOrEmpty(CurrentStoreId))
            {
                id = Guid.Parse(CurrentStoreId);
            }
            else
            {
                id = Guid.NewGuid();
            }
            Store store = new Store
            {
                StoreName = "My Store 1",
                Id = id
            };
            Store store1 = new Store
            {
                StoreName = "My Store 2",
                Id = Guid.NewGuid()
            };

            var stores = new ObservableCollection<Store> { store };

            HasMoreThanOneStore = stores.Count > 1 ? true : false;

            CreateHeader();

            return stores;
        }

        /// <summary>
        /// Create Shell Header
        /// A Picker and Image when more than one Store
        /// </summary>
        private void CreateHeader()
        {
            byte[] imageSourceBytes;
            Utils.GetByteArrayFromPath(out imageSourceBytes, App.ImagePlaceHolderPath);
            MemoryStream ms = new MemoryStream(imageSourceBytes);

            if (HasMoreThanOneStore)
            {
                var stack = new StackLayout()
                {
                    Padding = 5,
                    BackgroundColor = (Color)App.Current.Resources["PrimaryNavyBlue"],
                    Visual = VisualMarker.Material
                };

                //TODO: Bound Source to CurrentStoreLogo
                var image = new Image
                {
                    Aspect = Aspect.AspectFill,
                    Source = ImageSource.FromStream(() => ms),
                    Opacity = 0.7
                };

                ImagePicker imagePicker = new ImagePicker
                {
                    BackgroundColor = (Color)App.Current.Resources["WhiteBlue"],
                    DropDownImage = "dropdownarrow",
                    ItemsSource = MyStores,
                    PlaceHolderColor = Color.Black,
                    SelectedItem = CurrentStore,
                    TextColor = Color.Black
                };

                imagePicker.SetBinding(Picker.ItemsSourceProperty, nameof(MyStores));
                imagePicker.SetBinding(Picker.SelectedItemProperty, nameof(CurrentStore));
                imagePicker.ItemDisplayBinding = new Binding(".",BindingMode.Default,new StoresToStoreNameConverter());

                stack.Children.Add(image);
                stack.Children.Add(imagePicker);
                HeaderContent = stack;
            }
        }
        private void RegisterRoutes()
        {
            routes.Add("Home", typeof(HomePage));
            routes.Add("AddProductItem", typeof(AddProductItemPage));
            routes.Add("InventoryList", typeof(InventoryPage));
           

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        
        /// <summary>
        /// Sets CurrentStore
        /// if there is any currentstoreid saved localy 
        /// else get the first one of already fetched
        /// </summary>
        private void SetCurrentStore()
        {
            // CurrentStoreId is null or empty it was never saved in preferences
            if (string.IsNullOrEmpty(CurrentStoreId) || CurrentStoreId == Guid.Empty.ToString())
            {
                //Set Current Store something, the first of Current User stores
                CurrentStore = MyStores.FirstOrDefault();
            }
            else
            {
                //If preference has a CurrentUserId then get  from already fetched stores the current one
                CurrentStore = MyStores.Where(store => store.Id == Guid.Parse(CurrentStoreId)).FirstOrDefault();
            }
        }
    }
}
