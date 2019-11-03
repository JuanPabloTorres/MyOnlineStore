using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Users
{
    [Table("Users")]
    public abstract class BaseUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;

        public string Discriminator { get; private set; }
    }
}
