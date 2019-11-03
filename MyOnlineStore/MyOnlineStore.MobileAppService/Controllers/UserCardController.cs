using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Users;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardController : ControllerBase
    {
        private readonly IUserCardRepository _UserCardRepo;
        public UserCardController(IUserCardRepository userCardRepository)
        {
            _UserCardRepo = userCardRepository;
        }

        // GET: api/UserCard/5
        [HttpGet("{id}")]
        public string Get(string userId)
        {
            return "value";
        }

        // POST: api/UserCard
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool Post([FromBody] UserCard userCard)
        {
            _UserCardRepo.Add(userCard);

            return true;
        }

        // PUT: api/UserCard/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string cardId)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string cardId)
        {
        }
    }
}
