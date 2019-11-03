using MyOnlineStore.Shared.Models.Roles;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models
{
    [Table("RolesTypes")]
    public class RoleType
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }
        public ICollection<Role> Roles { get; set; }

    }
}
