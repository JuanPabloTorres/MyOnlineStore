using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Users;


namespace MyOnlineStore.MobileAppService.Repositories
{
    public class UserCardRepository : Repository<UserCard>, IUserCardRepository
    {
        public UserCardRepository(MyContext context) : base(context)
        {
        }
    }
}
