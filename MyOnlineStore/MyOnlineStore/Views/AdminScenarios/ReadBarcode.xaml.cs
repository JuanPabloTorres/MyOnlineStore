using MyOnlineStore.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadBarcode : ContentPage
    {

        ProductTest ProductTest;
        public ReadBarcode(ProductTest product)
        {
            InitializeComponent();
            ProductTest = product;


        }

        public  void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", result.Text, "OK");
                BarcodeDataStore dataStore = new BarcodeDataStore();

                //ProductTest product = new ProductTest();
                ProductTest = dataStore.GetItem(result.ToString());
               
                await App.Current.MainPage.Navigation.PopAsync();

                MessagingCenter.Send(ProductTest, "msg");
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _scanView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _scanView.IsScanning = false;
        }
    }
}