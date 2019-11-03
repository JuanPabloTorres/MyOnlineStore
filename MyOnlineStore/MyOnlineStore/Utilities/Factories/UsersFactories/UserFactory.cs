using MyOnlineStore.Entries;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Utilities.Factories.UsersFactories
{
    //==========================================================================================
    //
    // TODO: Assign roles when created
    //
    //==========================================================================================
    public class UserFactory : IUserFactory
    {
        public AdminUser CreateSimpleAdminUser() { return new AdminUser(); }

        public AdminUser CreateSimpleAdminUser(RegisterEntry registerModel)
        {
            throw new NotImplementedException();
        }

        public ClientUser CreateSimpleClientUser() { return new ClientUser(); }

        public ClientUser CreateSimpleClientUser(RegisterEntry registerModel)
        {
            return new ClientUser
            {
                UserName = registerModel.Email.Substring(0,registerModel.Email.IndexOf('@')),
                Email = registerModel.Email,
                AccessFailedCount = 0,
                BirthDate = registerModel.BirthDate,
                FullName = registerModel.FullName,
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = registerModel.Email.ToLower(),
                NormalizedUserName = registerModel.Email.Substring(0,registerModel.Email.IndexOf('@')).ToLower(),
                PhoneNumber = registerModel.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            };
        }

        public EmployeeUser CreateSimpleEmployeeUser() { return new EmployeeUser(); }

        public EmployeeUser CreateSimpleEmployeeUser(RegisterEntry registerModel)
        {
            return new EmployeeUser
            {
                UserName = registerModel.Email.Substring(registerModel.Email.IndexOf('@')),
                Email = registerModel.Email,
                AccessFailedCount = 0,
                BirthDate = registerModel.BirthDate,
                FullName = registerModel.FullName,
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = registerModel.Email.ToLower(),
                NormalizedUserName = registerModel.Email.Substring(registerModel.Email.IndexOf('@')).ToLower(),
                PhoneNumber = registerModel.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            };
        }
    }
}
