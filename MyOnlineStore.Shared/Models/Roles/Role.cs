using Microsoft.AspNetCore.Identity;
using MyOnlineStore.Shared.Models.Users;
//using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Roles
{
    //[JsonObject]
    [Table("UsersRoles")]
    public class Role: IdentityRole<Guid>
    {
        public Role() { }

        #region NoUse
        [NotMapped]
        private new string Name { get; set; }

        [NotMapped]
        private new string NormalizedName { get; set; }

        #endregion

        public int RoleReferenceId { get; set; }
        public RoleType RoleReference { get; set; }

        public Guid MyUserId { get; set; }
        public virtual EmployeeUser MyUser { get; set; }

        public string BussinesName { get; set; }

      
      
    }
}
