using MyOnlineStore.Interfaces.IViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.LoginScenario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<ILoginViewModel>();
        }
    }
}