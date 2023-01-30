using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Interfaces.IBusinessRepository;
using Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeCorner.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private IShopppingCartRepository _shoppingCartRepo;

      
        public ShoppingCartController(IShopppingCartRepository shoppingCartRepo)
        {
            _shoppingCartRepo = shoppingCartRepo;
        }

        [HttpGet]
        public async Task< ActionResult<ShoppingCart> > GetShoppingCartById(string id) {

            var shoppingCart = await _shoppingCartRepo.GetShoppingCartAsync(id);
            return Ok( (shoppingCart!=null)?shoppingCart: new ShoppingCart(id)  );
        }

        [HttpPost]
           public async Task< ActionResult<ShoppingCart>> UpdateShoppingCart([FromBody]ShoppingCart shoppingCart) {

            var updatedShoppingCart = await _shoppingCartRepo.UpdateShoppingCartAsync(shoppingCart);
            return Ok( updatedShoppingCart );
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteShoppingCart(string id)
        {
            await _shoppingCartRepo.DeleteShoppingCartAsync(id);

            return NoContent();
         }

    }
}
