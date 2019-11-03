using MyOnlineStore.Interfaces.ViewInterfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MyOnlineStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IShellViewModel>();
        }
    }
}
