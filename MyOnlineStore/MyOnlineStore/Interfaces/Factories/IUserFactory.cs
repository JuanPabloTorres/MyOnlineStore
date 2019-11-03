using MyOnlineStore.Entries;
using MyOnlineStore.Shared.Models.Users;

namespace MyOnlineStore.Interfaces.Factories
{
    public interface IUserFactory
    {
        ClientUser CreateSimpleClientUser();
        EmployeeUser CreateSimpleEmployeeUser();
        AdminUser CreateSimpleAdminUser();

        ClientUser CreateSimpleClientUser(RegisterEntry registerModel);
        EmployeeUser CreateSimpleEmployeeUser(RegisterEntry registerModel);
        AdminUser CreateSimpleAdminUser(RegisterEntry registerModel);
    }
}
