using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class RoleTypeRepository : Repository<RoleType>, IRoleRefRepository
    {
        public RoleTypeRepository(MyContext context) : base(context)
        {

        }

        public RoleType GetByRoleName(string roleName)
        {
            var role = _Context.Roles.Where(role => role.RoleName == roleName).SingleOrDefault();
            return role;
        }
    }
}
