using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.Shared.Models.Purchases;

namespace MyOnlineStore.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        protected readonly IProductItemRepository _ProductItemRepo;
        public ProductItemController(IProductItemRepository productItemRepository)
        {
            _ProductItemRepo = productItemRepository;
        }
        // GET: api/ProductItem
        [HttpGet]
        public IEnumerable<ProductItem> GetAll()
        {
            var resultList = _ProductItemRepo.GetAll();
            return resultList;
        }

        // GET: api/ProductItem/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProductItem
        [HttpPost]
        public void Create([FromBody] ProductItem item)
        {
           bool result = _ProductItemRepo.Add(item);
        }

        // PUT: api/ProductItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
