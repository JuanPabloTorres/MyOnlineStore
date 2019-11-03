using MyOnlineStore.Shared.CustomAttribute;
using MyOnlineStore.Shared.Models.Interfaces;
using MyOnlineStore.Shared.Models.Roles;
using MyOnlineStore.Shared.Models.Stores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineStore.Shared.Models.Users
{
    [ApiGenericEntity]
    public class EmployeeUser : ClientUser, IEmployeeUser
    {
        [Required]
        public ICollection<StoreEmployee> MyWorkStore { get; protected set; }
        [Required]
        public ICollection<Role> Roles { get; protected set; }
    }
}
