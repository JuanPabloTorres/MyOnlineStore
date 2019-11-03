using MyOnlineStore.Interfaces.IViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.LoginScenario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterAccountPage : ContentPage
	{
		public RegisterAccountPage ()
		{
			InitializeComponent ();

            BindingContext = Startup.ServiceProvider.GetService<IRegisterCardViewModel>();
		}
	}
}