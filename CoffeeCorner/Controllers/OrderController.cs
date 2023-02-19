using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinesssLogic.Data;
using CoffeeCorner.DTOs;
using CoffeeCorner.Errors;
using Domains.Interfaces.IServices;
using Domains.Interfaces.IUnitOfWork;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeCorner.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[action]")]



    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService ;
        private IMapper _mapper { get; }

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

        }


        [HttpPost]
        public async Task<ActionResult<OrderReturnedDTO>> CreateOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);
                var address = _mapper.Map<OrderAddress>(orderDTO.shippingAddress);

                var order = await _orderService.CreateOrderAsync(user, orderDTO.basketId, orderDTO.deliveryMethodId, address);

                if (order != null)
                    return Ok(_mapper.Map<OrderReturnedDTO>(order));
                else
                    return BadRequest(new ApiResponse(400));
            }catch(Exception ex)
            {
                return StatusCode(500,new ExceptionResponse(500,null,ex.Message) );
            }

        }


        [HttpGet]
        public async Task<ActionResult<OrderReturnedDTO>> GetUserOrders()
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var orders = await _orderService.GetUserOrders(user);

                if(orders!=null)
                    return Ok(_mapper.Map<List<OrderReturnedDTO>>(orders));
                else
                    return BadRequest(new ApiResponse(400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ExceptionResponse(500, null, ex.Message));
            }

        }

       [HttpGet]
        public async Task<ActionResult<OrderReturnedDTO>> GetUserOrderById(int orderId)
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var order = await _orderService.GetOrderById(user, orderId);

                if (order != null)
                    return Ok(_mapper.Map<OrderReturnedDTO>(order));
                else
                    return BadRequest(new ApiResponse(400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ExceptionResponse(500, null, ex.Message));
            }

        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDeliveryMethods>>> GetDeliveryMethods()
        {
            try
            {

                var deliveryMethods = await _orderService.GetDeliveryMethods();

                if (deliveryMethods != null)
                    return Ok(deliveryMethods);
                else
                    return BadRequest(new ApiResponse(400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ExceptionResponse(500, null, ex.Message));
            }

        }









    }
}
