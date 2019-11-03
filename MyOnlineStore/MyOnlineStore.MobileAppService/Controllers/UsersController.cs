using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.CustomAttribute;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Users;


namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [GenericControllerName]
    public class UsersController<TUser> : GenericController<TUser> where TUser : BaseUser
    {
        private readonly IUsersRepository<TUser> _UsersRepo;

        public UsersController(IUsersRepository<TUser> usersRepository)
        {
            _UsersRepo = usersRepository;
        }

        // GET: api/ClientUser/5
        [HttpGet("[action]/{id}")]
        public TUser GetById(Guid id)
        {
            var value = _UsersRepo.Get(id);
            return value;
        }

        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public TUser GetUserByType(string email)
        {
            var value = _UsersRepo.GetUserByType(email);
            return value;
        }

        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public TUser GetUserByEmail(string email)
        {
            var value = _UsersRepo.GetUserByEmail(email);
            return value;
        }

        // POST: api/ClientUser
        [HttpPost]
        public void Create([FromBody] TUser user)
        {
            if (user !=null)
            {
                _UsersRepo.Add(user);
            }           
        }

        // PUT: api/ClientUser/5
        [HttpPut("{id}")]
        public bool Put([FromBody]TUser user)
        {
            bool added;
            try
            {
                _UsersRepo.Update(user);
                added = true;
            }
            catch (Exception ex)
            {
                added = false;
                Console.WriteLine($"Exception in ClientUser Controller: {ex.Message}");
            }

            return added;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return false;
        }

        
    }
}
