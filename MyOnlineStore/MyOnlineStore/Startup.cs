using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xamarin.Essentials;
using MyOnlineStore.Utilities;
using MyOnlineStore.ViewModels;
using MyOnlineStore.Interfaces.ViewInterfaces;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Views.LoginScenario;
using MyOnlineStore.DataStore;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Views;
using MyOnlineStore.Utilities.Factories.UsersFactories;
using MyOnlineStore.Utilities.Factories.UserDataStoreFactories;
using MyOnlineStore.Entries;
using MyOnlineStore.Utilities.Factories.ValidatableObjectsFactories;
using MyOnlineStore.Testing.MockData.Entries;
using MyOnlineStore.ViewModels.LoginScenario;
using MyOnlineStore.ViewModels.Home;
using MyOnlineStore.ViewModels.Admin;
using MyOnlineStore.Utilities.Factories.ProductItemFactories;
using MyOnlineStore.Utilities.Factories.StoreFactories;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Views.AdminScenarios;

namespace MyOnlineStore
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {         
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource("MyOnlineStore.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "MyOnlineStore.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(fullConfig);
                })
                .ConfigureServices((c, x) =>
                {
                    nativeConfigureServices(c, x);
                    ConfigureServices(c, x);
                })
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
                .Build();

            ServiceProvider = host.Services;
            var app = ServiceProvider.GetService<App>();
            return app;
        }


        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                var world = ctx.Configuration["Hello"];
            }
            else
            {

            }

            services.AddHttpClient("MyHttpClient", client =>
            {
                client.BaseAddress = new Uri($"{App.LocalBackendUrl}/");
            });

          
            services.AddSingleton(typeof(IUserDataStore<>),typeof(UserDataStore<>));
            services.AddSingleton<IRoleDataStore, RoleDataStore>();
            services.AddSingleton<AppShell>();
            services.AddSingleton<App>();
            services.AddSingleton<IRegisterCardViewModel,RegisterCardViewModel>();         
            services.AddSingleton<RegisterAccountPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<AdministratorViewModel>();
            services.AddSingleton<IUserCardDataStore, UserCardDataStore>();
            services.AddSingleton<ILoginViewModel, LoginViewModel>();
            services.AddSingleton<IExploreStoresViewModel, ExploreStoresViewModel>();
            services.AddSingleton<IHomeViewModel, HomeViewModel>();
            services.AddSingleton<Global.AppTheme>();
            services.AddSingleton<LoginEntry>();
            services.AddSingleton<RegisterEntry>();
            services.AddSingleton<MockRegisterEntry>();
            services.AddSingleton<UserCardEntry>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IUserDataStoreFactory, UserDataStoreFactory>();
            services.AddSingleton<IValidatableObjectFactory, ValidatableObjectsFactory>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<IShellViewModel, ShellViewModel>();
            services.AddTransient<RegisterPage>();
            services.AddSingleton<Page1>();
            services.AddSingleton<IStoreDataStore, StoreDataStore>();
            services.AddSingleton<IRegisterStoreViewModel, RegisterStoreViewModel>();

            services.AddSingleton<IReportViewModel, ReportViewModel>();
            services.AddSingleton<IOrdersDataStore, MockDataStoreOrder>();

            services.AddSingleton<AddProductItemViewModel>();
            services.AddSingleton<ProductTest>();
            services.AddSingleton<ReadBarcode>();

            services.AddSingleton<IProductItemDataStore,MockInventoryDataStore>();

            services.AddSingleton<IProductItemFactory, ProductItemFactory>();
            services.AddSingleton<IInventoryViewModel, InventoryViewModel>();
            services.AddSingleton<IStoreFactory, StoreFactory>();
            services.AddSingleton<IShellViewModel, ShellViewModel>();
        }
    }
}
