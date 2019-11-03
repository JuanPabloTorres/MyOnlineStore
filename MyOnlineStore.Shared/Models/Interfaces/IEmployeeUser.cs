using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Interfaces
{
    public interface IEmployeeUser: IRoleableUser
    {
        ICollection<StoreEmployee> MyWorkStore { get; }
    }
}
