using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Interfaces.Validations;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using MyOnlineStore.Views.Templates;
using MyOnlineStore.Utilities;
using MyOnlineStore.Shared.Models.Stores;

namespace MyOnlineStore.ViewModels.Admin
{

    //====================================================================================
    // 
    // TODO: Create a validation rule for checking if store owner name exist 
    //
    //====================================================================================
    public class RegisterStoreViewModel : BaseViewModel, IRegisterStoreViewModel
    {
        protected readonly IValidatableObjectFactory _validatableObjectFactory;
        protected readonly IUserDataStore<AdminUser> _adminUserDataStore;
        protected readonly IStoreDataStore _StoreDataStore;
        protected readonly IStoreFactory _StoreFactory;
        const bool pass = false;

        public ICommand NavigateToAddProductItemCommand { get; set; }
        public ICommand GetLocationCommand { get; set; }
        public ICommand StoreNameValidationCommand { get; set; }
        public ICommand LongitudeValidationCommand { get; set; }
        public ICommand LatitudeValidationCommand { get; set; }
        public ICommand StoreOwnerNameValidationCommand { get; set; }
        public ICommand MoreInfoCommand { get; set; }
        public ICommand RegisterStoreCommand { get; set; }
        public ICommand DescriptionValidationCommand { get; set; }
        public ICommand GetLogoPhotoCommand { get; set; }

        //------------------------------------------------------------------------------
        // TODO: Implement Take Photo Command
        public ICommand TakeLogoPhotoCommand { get; set; }

        public IValidateableObject<string> StoreName { get; set; }
        public IValidateableObject<string> CardNumber { get; set; }
        public IValidateableObject<string> Longitude { get; set; }
        public IValidateableObject<string> Latitude { get; set; }
        public IValidateableObject<string> StoreOwnerName { get; set; }
        public IValidateableObject<string> Description { get; set; }
        public IValidateableObject<string> Category { get; set; }

        private byte[] _productImageSource;
        public byte[] ProductImageSource
        {
            get { return _productImageSource; }
            set { _productImageSource = value; RaisePropertyChanged(() => ProductImageSource); }
        }
        public bool VerifiedCard { get; set; }
        public Store CurrentStore { get; set; } // = Current Store

        public RegisterStoreViewModel(IValidatableObjectFactory validatableObjectFactory, IUserDataStore<AdminUser> adminUserDataStore, IStoreDataStore storeDataStore, IStoreFactory storeFactory)
        {
            _validatableObjectFactory = validatableObjectFactory;
            _adminUserDataStore = adminUserDataStore;
            _StoreDataStore = storeDataStore;
            _StoreFactory = storeFactory;

            StoreName = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Store 1");
            CardNumber = _validatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Longitude = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("1");
            Latitude = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("2");
            StoreOwnerName = _validatableObjectFactory.CreateSimpleValidatebleObject<string>("Owner 1");
            Description = _validatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Category = _validatableObjectFactory.CreateSimpleValidatebleObject<string>();

            StoreNameValidationCommand = new Command(() => StoreName.Validate());
            LongitudeValidationCommand = new Command(() => Longitude.Validate());
            LatitudeValidationCommand = new Command(() => Latitude.Validate());
            StoreOwnerNameValidationCommand = new Command(() => StoreOwnerName.Validate());
            MoreInfoCommand = new Command<CommandEventData>((commandData) => MoreInfoPopup(commandData));
            NavigateToAddProductItemCommand = 
                new Command<CommandEventData>(async (data)=> await NavigateToAddProductItem());
            RegisterStoreCommand = new Command(()=>SaveStore());
            DescriptionValidationCommand = new Command(() => Description.Validate());
            GetLogoPhotoCommand = new Command(async () => ProductImageSource = await Utils.GetPhoto(ProductImageSource));
            TakeLogoPhotoCommand = new Command(async () => ProductImageSource = await Utils.TakePhoto(ProductImageSource));
            //Set Placeholder Image
            Utils.GetByteArrayFromPath(out _productImageSource, App.ImagePlaceHolderPath);

            //Landing Information Popup
            Task.Run(()=>MoreInfoCommand.Execute(null));
        }
        
        private async Task NavigateToAddProductItem(CommandEventData commandEventData = null)
        {           
            if (CurrentStore == null)
            {
                SaveStore();
                if (CurrentStore != null)
                    await Shell.Current.GoToAsync("InventoryList");
                else
                {
                    //Popup message: Message -> Cant Register store
                }
            }
            else
            {
                await Shell.Current.GoToAsync("InventoryList");
            }
        }

        public bool SaveStore()
        {
            bool storeExists;
            
            if (Validate())
            {   
                if (CurrentStore == null)
                {
                    storeExists = _StoreDataStore.StoreExists(StoreName.Value, StoreOwnerName.Value,
                        new Shared.Models.Stores.Location
                        {
                            Longitude = Longitude.Value,
                            Latitude = Latitude.Value
                        });

                    if (!storeExists)
                    {
                        //Create Store and set Current Store
                        CurrentStore = _StoreFactory.CreateStore(storeName: StoreName.Value, 
                            ownerName: StoreOwnerName.Value,
                            description: Description.Value, 
                            location: new Shared.Models.Stores.Location 
                            {
                                Latitude = Latitude.Value, 
                                Longitude = Longitude.Value 
                            }, 
                            category: Category.Value, 
                            logo: ProductImageSource);
                        CurrentStore = _StoreDataStore.AddItem(CurrentStore) ? CurrentStore : null;
                    }
                    else
                    {
                        //Get and Set to Current Store
                        CurrentStore = _StoreDataStore.GetStore(storeName: StoreName.Value, 
                            storeOwnerName: StoreOwnerName.Value, 
                            new Shared.Models.Stores.Location
                            { 
                                Longitude = Longitude.Value, 
                                Latitude = Latitude.Value
                            });
                    }
                }
            }

            storeExists = CurrentStore == null ? false : true;

            return storeExists;
        }

        

        public bool Validate()
        {
            return StoreName.Validate() && CardNumber.Validate() && Longitude.Validate() && Latitude.Validate() ;
        }

        

        public async Task GetLocation()
        {
            var location = await Geolocation.GetLocationAsync();
        }

        public void MoreInfoPopup(CommandEventData commandEventData)
        {
            PopupNavigation.Instance.PushAsync(new PopupView());
        }
    }
}
