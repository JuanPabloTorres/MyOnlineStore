using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Users
{
    [Table("UsersCards")]
    public class UserCard
    {
        public UserCard()
        {
            Id = Guid.NewGuid();
            Number = string.Empty;
            Exp = new DateTime();
            Pin = string.Empty;
            Type = string.Empty;
        }
        
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Exp { get; set; }
        public string Pin { get; set; }
        public string Type { get; set; }
        public bool IsVerified { get; set; }
    }
}
