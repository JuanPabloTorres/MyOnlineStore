using MyOnlineStore.Shared.Models.Roles;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Models.Interfaces
{
    public interface IRoleableUser
    {
        ICollection<Role> Roles { get; }
    }
}
