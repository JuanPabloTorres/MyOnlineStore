
using MyOnlineStore.Shared.Models.Users;


namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IUsersRepository<TUser> : IRepository<TUser> where TUser : BaseUser
    {
        // -----------------------------------------------------
        //
        // Write the Specific query for this Interface
        // Ex: GetTopClient(int count);
        //
        // -----------------------------------------------------

        TUser GetUserByType(string email);
        TUser GetUserByEmail(string email);
    }
}
