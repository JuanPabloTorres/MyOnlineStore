using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IRoleDataStore : IDataStoreService<Role>
    {
        RoleType GetByRoleName(string name);
    }
}
