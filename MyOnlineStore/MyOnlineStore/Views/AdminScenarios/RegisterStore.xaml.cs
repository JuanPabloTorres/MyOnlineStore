using MyOnlineStore.Interfaces.IViewModel;
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
    public partial class RegisterStore : ContentPage
    {
        public RegisterStore()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IRegisterStoreViewModel>();
        }
    }
}