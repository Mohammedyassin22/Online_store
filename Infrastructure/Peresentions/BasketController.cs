using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceBasket serviceBasket):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result =await serviceBasket.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto dto)
        {
            var result=await serviceBasket.UpdateBasketAsync(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)        {
            var result = await serviceBasket.DeleteBascketAsync(id);
            return NoContent();
        }
    }
}
