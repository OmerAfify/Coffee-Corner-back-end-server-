using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        public ShoppingCart(string id)
        {
            this.id = id;
        }

        public string id { get; set; }
        public List<CartItem> items { get; set; } = new List<CartItem>();


    }
}
