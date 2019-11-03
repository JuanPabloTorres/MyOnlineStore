using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Entries
{
   public class LoginEntry
    {
        public LoginEntry()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
