using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;

namespace Domains.Interfaces.IBusinessRepository
{
    public interface  IShopppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId);
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCartAsync(string shoppingCartId);

    }
}
