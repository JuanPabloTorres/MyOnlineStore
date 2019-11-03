using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IHomeViewModel>();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
             Shell.Current.GoToAsync("detail");
        }
    }
}