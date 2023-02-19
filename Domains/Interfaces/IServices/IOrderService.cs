using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;

namespace Domains.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(string buyerEmail, string shoppingCartId,
                                             int deliveryMethodId, OrderAddress orderAddress);

        public Task<Order> GetOrderById(string buyerEmail, int orderId);
        public Task<IEnumerable<Order>> GetUserOrders(string email);
        public Task<IEnumerable<OrderDeliveryMethods>> GetDeliveryMethods();




    }

}
