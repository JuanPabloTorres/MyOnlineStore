using MyOnlineStore.Shared.Models.Interfaces;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Shared.Models.Stores
{
    [Table("StoresEmployees")]
    public class StoreEmployee
    {
        public Guid UserId { get; set; }
        public EmployeeUser EmployeeUser { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
