using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domains.Interfaces.IBusinessRepository;
using Domains.Models;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace BusinessLogic.Repository.BusinessRepository
{
    public class ShoppingCartRepository : IShopppingCartRepository
    {
        private IDatabase _datbase { get; set; }

        public ShoppingCartRepository(IConnectionMultiplexer redis)
        {
            _datbase = redis.GetDatabase();
        }



        public async Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId)
        {
            var cart = await _datbase.StringGetAsync(shoppingCartId);

            return (!cart.IsNullOrEmpty) ? JsonSerializer.Deserialize<ShoppingCart>(cart) : null;
        }

        
        public async Task<ShoppingCart> UpdateShoppingCartAsync( ShoppingCart shoppingCart)
        {
            var cart = await _datbase.StringSetAsync(shoppingCart.id, JsonSerializer.Serialize(shoppingCart), TimeSpan.FromDays(30));

            return (!cart) ? null : await GetShoppingCartAsync(shoppingCart.id);
        }

        public async Task<bool> DeleteShoppingCartAsync(string shoppingCartId)
        {
            return await _datbase.KeyDeleteAsync(shoppingCartId);
        }
    }
}


