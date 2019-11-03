using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Utilities
{
    public class UserTuple : Tuple<dynamic, string, string, Type>
    {
        private dynamic instance;
        public dynamic Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string pass;
        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        private Type typeofUser;
        public Type TypeOfUser
        {
            get { return typeofUser; }
            set { typeofUser = value; }
        }

        
        public UserTuple(dynamic currentUser, string email, string pass, Type typeofUser) 
            : base((BaseUser)currentUser, email, pass, typeofUser)
        {
            Instance = Item1;
            Email = Item2;
            Password = Item3;
            TypeOfUser = Item4;
        }

        public static bool HasValues(UserTuple userTuple)
        {
            bool validated = false;

            if (userTuple!=null)
            {
                if (string.IsNullOrEmpty(userTuple.Email) && string.IsNullOrEmpty(userTuple.Password) && userTuple.TypeOfUser!=null && userTuple.Instance!=null)
                {
                    validated = true;
                }
            }

            return validated;
        }
    }
}
