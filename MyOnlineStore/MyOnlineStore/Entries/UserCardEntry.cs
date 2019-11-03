using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Entries
{
    public class UserCardEntry
    {
        public UserCardEntry()
        {
            Number = string.Empty;
            Exp = new DateTime();
            Pin = string.Empty;
            Type = string.Empty;
        }
        public string Number { get; set; }
        public DateTime Exp { get; set; }
        public string Pin { get; set; }
        public string Type { get; set; }

        public UserCard ToUserCard()
        {
            return new UserCard
            {
                Number = this.Number,
                Exp = this.Exp,
                Pin = this.Pin,
                Type = this.Type
            };
        }
    }
}
