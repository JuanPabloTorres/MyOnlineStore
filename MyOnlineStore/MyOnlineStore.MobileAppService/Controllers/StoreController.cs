using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Stores;
using MyOnlineStore.Shared.Models.Users;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoresRepository _StoreRepository;
        public StoreController(IStoresRepository storesRepository)
        {
            _StoreRepository = storesRepository;
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Store GetbyId(Guid id)
        {
            var result = _StoreRepository.Get(id);
            return result;
        }

        [HttpGet("[action]/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Store GetByName(string name)
        {
            var result = _StoreRepository.GetByName(name);
            return result;
        }


        [HttpGet("[action]/{ownerName}/{storeName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Store> GetStore([FromRoute]string ownerName, [FromRoute]string storeName,[FromBody]Location location)
        {
            var result = await _StoreRepository.GetStore(name: storeName, ownerName: ownerName, location: location);
            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Store> GetAll()
        {
            var result = _StoreRepository.GetAll();
            return result;
        }

        [HttpGet("[action]/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Store> GetClientFavorites(Guid clientId)
        {
            var result = _StoreRepository.GetClientFavorites(clientId);
            return result;
        }

        [HttpGet("[action]/{storeowner}/{storename}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool StoreExists([FromRoute]string storeOwner, [FromRoute]string storeName, [FromBody]Location location)
        {
            bool exists = _StoreRepository.StoreExists(storeOwner, storeName, location);
            return exists;
        }

        //=============================================================================
        //
        // TODO: AddStore,GetClientsStoresNearBy, UpdateStore
        //
        //=============================================================================
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public bool CreateStore([FromBody]Store store)
        {
            bool exists = _StoreRepository.Add(store);
            return exists;
        }
    }
}