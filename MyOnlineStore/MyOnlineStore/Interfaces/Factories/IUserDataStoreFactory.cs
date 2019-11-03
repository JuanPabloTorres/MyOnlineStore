using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Users;

namespace MyOnlineStore.Interfaces.Factories
{
    public interface IUserDataStoreFactory
    {
        IUserDataStore<ClientUser> GetClientUserDataStore();
        IUserDataStore<EmployeeUser> GetEmployeeUserDataStore();
        IUserDataStore<AdminUser> GetAdminUserDataStore();
    }
}
