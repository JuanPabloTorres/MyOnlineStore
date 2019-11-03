using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Validations;
using MyOnlineStore.ViewModels.Base;
using MyOnlineStore.Views.AdminScenarios;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.ViewModels.Admin
{
    //==================================================================================
    //
    // 
    // TODO: In Progress -> Adding item to db. Need MyStore field for relation.
    // TODO: Where to save Current Store? -> Xamarin.Essentials.Storage? 
    //
    //==================================================================================
    public class AddProductItemViewModel : BaseViewModel
    {
        protected readonly IProductItemDataStore _ProductItemDataStore;
        protected readonly IProductItemFactory _ProductFactory;
        protected readonly IValidatableObjectFactory _ValidatableObjectFactory;
        
        public ICommand GetProductPhotoCommand { get; set; }

        //------------------------------------------------------------------------------
        // TODO: Implement Take Photo Command
        public ICommand TakeProductPhotCommand { get; set; }
        public ICommand ReadBarcodeCommand { get; set; }
        public ICommand ValidateItemNameCommand { get; set; }
        public ICommand ValidatePriceCommand { get; set; }
        public ICommand ValidateDescriptionCommand { get; set; }
        public ICommand ValidateQuantityCommand { get; set; }
        public ICommand ValidateTypeOfItemCommand { get; set; }
        public ICommand AddToInventoryCommand { get; set; }

        ValidatableObject<string> itemname;
       public ValidatableObject<string> ItemName {

            get
            { return itemname; }

            set
            {
                itemname = value;
                RaisePropertyChanged(()=>ItemName);
            }
        
        }
        ValidatableObject<string> Price { get; set; }

        ValidatableObject<string> description;
       public ValidatableObject<string> Description {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        ValidatableObject<string> Quantity { get; set; }
        ValidatableObject<string> TypeOfItem { get; set; }

        private byte[] _productImageSource;
        public byte[] ProductImageSource 
        { 
            get { return _productImageSource; }
            set { _productImageSource = value; RaisePropertyChanged(() => ProductImageSource); }
        }

        private ProductTest test = Startup.ServiceProvider.GetService<ProductTest>();
        public ProductTest Test
        {
            get => test;
            set
            {
                test = value;
            }
        }
        public AddProductItemViewModel(IProductItemDataStore productItemDataStore, IProductItemFactory productItemFactory,IValidatableObjectFactory validatableObjectFactory,ProductTest product)
        {
            _ProductItemDataStore = productItemDataStore;
            _ProductFactory = productItemFactory;
            _ValidatableObjectFactory = validatableObjectFactory;

            ItemName = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("Item 1");
            Price = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("1");
            Description = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("Desc 1");
            Quantity = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("10");
            TypeOfItem = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>("Type 1");

            ValidateItemNameCommand = new Command(() => ItemName.Validate());
            ValidatePriceCommand = new Command(() => Price.Validate());
            ValidateDescriptionCommand = new Command(() => Description.Validate());
            ValidateQuantityCommand = new Command(() => Quantity.Validate());
            ValidateTypeOfItemCommand = new Command(() => TypeOfItem.Validate());

            GetProductPhotoCommand = new Command(async () => ProductImageSource = await Utilities.Utils.GetPhoto(ProductImageSource));
            TakeProductPhotCommand = new Command(async () => ProductImageSource = await Utilities.Utils.TakePhoto(ProductImageSource));
            ReadBarcodeCommand = new Command(() =>
            {

                App.Current.MainPage.Navigation.PushAsync(Startup.ServiceProvider.GetService<ReadBarcode>());
            
            });

            Test = new ProductTest();
            Test = product;

            // Get Image Placeholder
            Utilities.Utils.GetByteArrayFromPath(out _productImageSource, App.ImagePlaceHolderPath);

            AddToInventoryCommand = new Command(async()=>   await InsertProductItemToDB());

            MessagingCenter.Subscribe<ProductTest>(this, "msg", (ProductTest) =>
            {
                Test = ProductTest;

                foreach (var item in test.items)
                {
                    ItemName.Value = item.title;
                    Description.Value = item.description;

                    ProductImageSource = Encoding.ASCII.GetBytes(item.images[0]);



                }
            });
        }
        private async Task InsertProductItemToDB()
        {
            var item = _ProductFactory.CreateProductItem(itemName: ItemName.Value,
                price: Price.Value, 
                description: Description.Value, 
                quantity: Quantity.Value, 
                type: TypeOfItem.Value, 
                logo: ProductImageSource);

            //---------------------------------------------------------------------------------------------
            // TODO: If Item exists pop confirm message of adding quantity and other properties
            _ProductItemDataStore.AddItem(item);
        }
        //private async Task GetProductPhoto()
        //{
        //    MediaFile mediaFile;
        //    MemoryStream memoryStream;
        //    if (CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Full, CompressionQuality = 85 });

        //        if (mediaFile!=null)
        //        {
        //            memoryStream = new MemoryStream();
        //            mediaFile.GetStream().CopyTo(memoryStream);
        //            ProductImageSource = memoryStream.ToArray();
        //        }
        //    }
        //    else
        //    {
        //        //--------------------------------------
        //        //
        //        // TODO: Inject PopupView and display 
        //        //       message of permission.
        //        //
        //        //--------------------------------------
        //        return;
        //    }
        //}
    }
}
