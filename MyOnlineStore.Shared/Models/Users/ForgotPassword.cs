using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Users
{
     public class ForgotPassword
    {
        public ForgotPassword()
        {
            UserEmail = string.Empty;
            Code = string.Empty;
            VerifyCode = string.Empty;
            ExpirationDate = new DateTime();
            DateCreate = new DateTime();
        }
        public string UserEmail { get; set; }
        public string Code { get; set; }
        public string VerifyCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
