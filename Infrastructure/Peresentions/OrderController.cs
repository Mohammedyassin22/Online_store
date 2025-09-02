using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceProduct serviceProduct):ControllerBase
    {
        [HttpPost]
        public async Task <IActionResult> CreateOrder(OrderRequestDto orderRequest)
        {
            var email =  User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceProduct.orderServices.CreateOrderAsync(email, orderRequest);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceProduct.orderServices.GetOrderEmail(email);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdForUser(Guid id)
        {
            var result = await serviceProduct.orderServices.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var result = await serviceProduct.orderServices.GetDeliveryMethodsAsync();
            return Ok(result);
        }
    }

}
