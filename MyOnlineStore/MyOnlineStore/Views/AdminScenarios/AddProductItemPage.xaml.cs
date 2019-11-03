using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductItemPage : ContentPage
    {
        public AddProductItemPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<AddProductItemViewModel>();
        }
        public AddProductItemPage(ProductTest product)
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<AddProductItemViewModel>();
        }

       
    }
}