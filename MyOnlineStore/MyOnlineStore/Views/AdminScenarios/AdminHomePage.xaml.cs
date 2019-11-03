using MyOnlineStore.Views.AdminScenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Administrator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : ContentPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }

        private async void GoInventory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage());
        }

        private async void GoAddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductItemPage());
        }

        private async void goReports(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportPage());
        }

        private async void goSetting(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Setting());
        }
    }
}