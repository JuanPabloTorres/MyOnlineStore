using MyOnlineStore.Shared.CustomAttribute;
using MyOnlineStore.Shared.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineStore.Shared.Models.Users
{
    [ApiGenericEntity]
    public class AdminUser : EmployeeUser,IAdminUser
    {
        public AdminUser() { }
    }
}
