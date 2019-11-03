using MyOnlineStore.Shared.Models;
namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IRoleRefRepository : IRepository<RoleType>
    {
        RoleType GetByRoleName(string roleName);
    }
}
