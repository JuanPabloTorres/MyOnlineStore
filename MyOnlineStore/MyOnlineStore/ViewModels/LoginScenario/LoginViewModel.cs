using MyOnlineStore.Entries;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Utilities;
using MyOnlineStore.ViewModels.Base;
using MyOnlineStore.Views.LoginScenario;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.ViewModels.LoginScenario
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        protected readonly IUserDataStoreFactory _UserDataStoreFactory;
        public LoginEntry LoginCredentials { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand NavigateToRegisterCommand { get; set; }
        public LoginViewModel(LoginEntry login,IUserDataStoreFactory userDataStoreFactory)
        {
            _UserDataStoreFactory = userDataStoreFactory;
            LoginCredentials = login;
            LoginCommand = new Command(async ()=> await Login());
            NavigateToRegisterCommand = new Command(()=> NavigateToRegisterPage());
        }

        public async Task Login()
        {
            IsBusy = true;
            IUserDataStore<ClientUser> userDataStore;
            userDataStore = _UserDataStoreFactory.GetClientUserDataStore();

            if (!(string.IsNullOrEmpty(LoginCredentials.Email) && string.IsNullOrEmpty(LoginCredentials.Password)))
            {
                var user = userDataStore.GetUserByEmail(LoginCredentials.Email);
                await App.SetCredentialsInStorage(new UserTuple(user, LoginCredentials.Email, LoginCredentials.Password, Type.GetType(user.Discriminator)));
                await App.SetCurrentUserData();
            }

            IsBusy = false;
            var x = App.CurrentUserData;

        }

        public void NavigateToRegisterPage()
        {
            // await Navigation.PushAsync(new RegisterPage());
            //await Application.Current.MainPage.Navigation.PushAsync(Startup.ServiceProvider.GetService<RegisterPage>());
            Application.Current.MainPage = Startup.ServiceProvider.GetService<RegisterPage>();
        }

        //Metodo para validacion
        public bool LoginCredentialsExist()
    {

        bool exists;
        //Check is user login credential exists.
        return true;
    }
      
    }
}
