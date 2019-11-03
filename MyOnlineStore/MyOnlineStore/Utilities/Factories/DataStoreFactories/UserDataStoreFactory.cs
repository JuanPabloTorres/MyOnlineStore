using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MyOnlineStore.Utilities.Factories.UserDataStoreFactories
{
    public class UserDataStoreFactory : IUserDataStoreFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public UserDataStoreFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IUserDataStore<AdminUser> GetAdminUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<AdminUser>>();
        }

        public IUserDataStore<ClientUser> GetClientUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<ClientUser>>();
        }

        public IUserDataStore<EmployeeUser> GetEmployeeUserDataStore()
        {
            return _serviceProvider.GetService<IUserDataStore<EmployeeUser>>();
        }
    }
}
