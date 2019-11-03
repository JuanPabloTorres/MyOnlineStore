using Microsoft.EntityFrameworkCore;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class UsersRepository<TUser> : Repository<TUser>, IUsersRepository<TUser> where TUser: BaseUser
    {
        public UsersRepository(MyContext context) 
            : base(context)
        {

        }

        public new async Task<IEnumerable<TUser>> GetAll()
        {
            return await _Context.Set<TUser>().ToListAsync();
        }

        public TUser GetUserByType(string email)
        {
            BaseUser user;

            if (typeof(TUser) == typeof(AdminUser))
            {
                user = _Context.Users
                    .Where(u => u.Email == email && u.Discriminator == typeof(AdminUser).Name)
                    .FirstOrDefault();
            }
            else if (typeof(TUser) == typeof(EmployeeUser))
            {
                user = _Context.Users
                    .Where(u => u.Email == email && u.Discriminator == typeof(EmployeeUser).Name)
                    .FirstOrDefault();
            }
            else //ClientUser
            {
                user = _Context.Users
                    .Where(u => u.Email == email && u.Discriminator == typeof(ClientUser).Name)
                    .FirstOrDefault();
            }

            return user as TUser;
        }

        public TUser GetUserByEmail(string email)
        {
            var user = _Context.Users.Where(user => user.Email == email).FirstOrDefault();
            return user as TUser;
        }
    }
}
