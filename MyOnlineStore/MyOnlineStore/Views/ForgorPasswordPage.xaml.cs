using MyOnlineStore.ViewModels.LoginScenario;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgorPasswordPage : ContentPage
	{
		public ForgorPasswordPage ()
		{
			InitializeComponent ();
            BindingContext = new ForgotViewModel(Navigation);
		}
	}
}