using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Interfaces.IBusinessRepository;
using Domains.Interfaces.IServices;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopppingCartRepository _shoppingCartRepository;

        public OrderService(IUnitOfWork unitOfWork, IShopppingCartRepository shopppingCartRepository)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartRepository = shopppingCartRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string shoppingCartId,
            int deliveryMethodId, OrderAddress orderAddress)
        {
            // get basket
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(shoppingCartId);

            var orderedItemsList = new List<OrderedItem>();

            foreach (var cartItem in shoppingCart.items)
            {
                var productItem = await _unitOfWork.Products.GetByIdAsync(cartItem.id);

                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.ProductName, productItem.PictureUrl, productItem.SalesPrice);

                var orderedItem = new OrderedItem(productItemOrdered, cartItem.qty, cartItem.qty * cartItem.price);

                orderedItemsList.Add(orderedItem);
            }

            var subtotal = orderedItemsList.Sum(o => o.TotalPrice);
            var deliveryMethod = await _unitOfWork.DeliveryMethods.GetByIdAsync(deliveryMethodId);

            var order = new Order(orderedItemsList,buyerEmail, orderAddress, subtotal, subtotal + (decimal)deliveryMethod.DeliveryPrice,
            1, deliveryMethodId);


            _unitOfWork.Orders.InsertAsync(order);


            var result = await _unitOfWork.Save();

            if (result <= 0)
                return null;
            else
                return order;

        }



        public async Task<Order> GetOrderById(string buyerEmail, int orderId)
        {
            var order = await _unitOfWork.Orders.
                FindAsync(o => o.Email == buyerEmail && o.OrderId == orderId,
                new List<string>(){ "DeliveryMethod", "OrderStatus", "OrderedItems"});


        return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string email)
        {
            var orders = await _unitOfWork.Orders.FindRangeAsync(o => o.Email == email,
                    new List<string>() { "DeliveryMethod", "OrderStatus", "OrderedItems" },
                    o => o.OrderByDescending(o => o.OrderDate)
                    ); ;


            return orders;
        }


        public async Task<IEnumerable<OrderDeliveryMethods>> GetDeliveryMethods()
        {

            return await _unitOfWork.DeliveryMethods.GetAllAsync(); 
        }

    }

}
