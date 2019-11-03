using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Microsoft.Extensions.Configuration;
using MyOnlineStore.Views.LoginScenario;
using Xamarin.Forms;
using MyOnlineStore.Views;
using System.Threading.Tasks;
using System;
using MyOnlineStore.Views.Administrator;
using MyOnlineStore.Views.AdminScenarios;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Utilities;

namespace MyOnlineStore
{
    public partial class App : Application
    {
       
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string ServerBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.10.120:44100" : "http://192.168.10.120:44100";
        public static string LocalBackendUrl =
         DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://192.168.10.120:44100";

        protected readonly IUserDataStoreFactory _UserDataStoreFactory;
        public readonly static string ImagePlaceHolderPath = "MyOnlineStore.Images.placeholder_images.png";
        public readonly static string SecureStoreageEmailKey = "auth_token_email";
        public readonly static string SecureStoreagePassKey = "auth_token_pass";
        public readonly static string SecureStoreageDiscKey = "auth_token_disk";

       public static UserTuple CurrentUserData { get; set; }

        public App()
        {
            InitializeComponent();
            _UserDataStoreFactory = Startup.ServiceProvider.GetService<IUserDataStoreFactory>();
            bool hasLoginCredentials = HasStorageCredentials().GetAwaiter().GetResult();

            
            if (true)// hasLoginCredentials
            {
                SetCurrentUserData();
                //MainPage =  Startup.ServiceProvider.GetService<RegisterPage>();
                //MainPage = Startup.ServiceProvider.GetService<RegisterAccountPage>();
                MainPage = Startup.ServiceProvider.GetService<AppShell>();
                //MainPage = Startup.ServiceProvider.GetService<Page1>();
                //MainPage = new AddProductItemPage();
            }
            else
            {               
                // To login 
                MainPage = Startup.ServiceProvider.GetService<LoginPage>();
            }
            
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
        /// <summary>
        /// Checks if storage has credentials. If does, sets CurrentUserEmail Id for CurrentUser fetching
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> HasStorageCredentials()
        {
            bool hasCred = false;
            try
            {
                var email = await SecureStorage.GetAsync(SecureStoreageEmailKey);
                var pass = await SecureStorage.GetAsync(SecureStoreagePassKey);
                var discriminator = await SecureStorage.GetAsync(SecureStoreageDiscKey);

                if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(pass))
                {
                    hasCred = false;
                }
                else { hasCred = true; }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return hasCred;
        }

        /// <summary>
        /// Fetches the User by the discriminator
        /// </summary>
        private static void FetchCurrentUser()
        {
            IUserDataStoreFactory userDataStoreFactory = Startup.ServiceProvider.GetService<IUserDataStoreFactory>();
            dynamic userDataStore;

            if (CurrentUserData.TypeOfUser == typeof(ClientUser))
            {
                userDataStore = userDataStoreFactory.GetClientUserDataStore();

                if (userDataStore is IUserDataStore<ClientUser> clientUserDataStore)
                {
                    CurrentUserData.Instance = clientUserDataStore.GetUserByType(CurrentUserData.Email);
                }
            }
            else if (CurrentUserData.TypeOfUser == typeof(EmployeeUser))
            {
                userDataStore = userDataStoreFactory.GetClientUserDataStore();

                if (userDataStore is IUserDataStore<EmployeeUser> clientUserDataStore)
                {
                    CurrentUserData.Instance = clientUserDataStore.GetUserByType(CurrentUserData.Email);
                }
            }
            else if (CurrentUserData.TypeOfUser == typeof(AdminUser))
            {
                userDataStore = userDataStoreFactory.GetClientUserDataStore();

                if (userDataStore is IUserDataStore<AdminUser> clientUserDataStore)
                {
                    CurrentUserData.Instance = clientUserDataStore.GetUserByType(CurrentUserData.Email);
                }
            }

        }

        /// <summary>
        /// Sets data to SecureStorage
        /// </summary>
        /// <param name="email">User email/key</param>
        /// <param name="pass">User password</param>
        /// <param name="discriminator">User type</param>
        /// <returns></returns>
        public static async Task SetCredentialsInStorage(string email=null,string pass=null,string discriminator=null)
        {
            if (string.IsNullOrEmpty(email))
            {
                await SecureStorage.SetAsync(App.SecureStoreageEmailKey, email);
            }
            else if (string.IsNullOrEmpty(pass))
            {
                await SecureStorage.SetAsync(App.SecureStoreagePassKey, pass);
            }
            else if (string.IsNullOrEmpty(discriminator))
            {
                await SecureStorage.SetAsync(App.SecureStoreageDiscKey, discriminator);
            }
        }

        /// <summary>
        /// Sets data to SecureStorage
        /// </summary>
        /// <param name="currentUserData">User data to save in secure storage</param>
        /// <returns></returns>
        public static async Task SetCredentialsInStorage(UserTuple currentUserData)
        {
            if (string.IsNullOrEmpty(currentUserData.Email))
            {
                await SecureStorage.SetAsync(App.SecureStoreageEmailKey, currentUserData.Email);
            }
            else if (string.IsNullOrEmpty(currentUserData.Password))
            {
                await SecureStorage.SetAsync(App.SecureStoreagePassKey, currentUserData.Password);
            }
            else if (currentUserData.TypeOfUser != null)
            {
                await SecureStorage.SetAsync(App.SecureStoreageDiscKey, currentUserData.TypeOfUser.Name);
            }
        }

        /// <summary>
        /// Sets CurrentUserData using Secure Storage
        /// Calls FetchCurrentUser() to set CurrentUserData.CurrentUser and completes CurrentUserData setting
        /// </summary>
        /// <returns></returns>
        public static async Task SetCurrentUserData()
        {
            CurrentUserData.Email =  SecureStorage.GetAsync(App.SecureStoreageEmailKey).Result;
            CurrentUserData.Password = SecureStorage.GetAsync(App.SecureStoreagePassKey).Result;

            var discriminator = SecureStorage.GetAsync(App.SecureStoreageDiscKey).Result;
            if (string.IsNullOrEmpty(discriminator))
            {
                CurrentUserData.TypeOfUser = Type.GetType(discriminator);
            }

            FetchCurrentUser();
        }
    }
}
