using MyOnlineStore.Shared.Models.Purchases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MyOnlineStore.Interfaces.IViewModel;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();           
            BindingContext = Startup.ServiceProvider.GetService<IInventoryViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //(BindingContext as IInventoryViewModel).InventoryItems = (BindingContext as IInventoryViewModel)

            MessagingCenter.Send(this, "update");
        }
    }
}