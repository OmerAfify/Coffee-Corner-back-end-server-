using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeCorner.DTOs
{
    public class OrderDTO
    {
        public string basketId { get; set; }
        public int deliveryMethodId { get; set; }
        public OrderAddressDTO shippingAddress { get; set; }
    }
}
