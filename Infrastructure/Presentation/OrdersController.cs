using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servies.Abstractions;
using Shared.OrderModels;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpPost] // Post : /api/Orders
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.orderService.CreateOrderAsync(request, email);
            return Ok(result);
        }
        [HttpGet] // Get : /api/Orders
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.orderService.GetOrderByUserEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("{id}")] // Get : /api/Orders/id
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await serviceManger.orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("DeliveryMethods")] // Get : /api/Orders/DeliveryMethods
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await serviceManger.orderService.GetAllDeliveryMethods();
            return Ok(result);
        }
    }

}
