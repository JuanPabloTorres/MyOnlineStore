using MyOnlineStore.Interfaces;
using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IUserDataStore<TUser> : IDataStoreService<TUser> where TUser : BaseUser
    {
        TUser GetUserByType(string email);
        TUser GetByEmailWithAll(string email, Type type);
        TUser GetUserByEmail(string email);
    }
}
