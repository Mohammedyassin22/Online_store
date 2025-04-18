using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceProduct serviceProduct):ControllerBase
    {
        [HttpGet]
        public   async Task <IActionResult> GetAllProducts(int? brandid , int? typeid,string? sort)
        {
            var result = await serviceProduct.Services.GetAllProductsAsync(brandid, typeid,sort);
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("product/{id:int}")]
        public async Task <IActionResult>GetProductById(int id)
        {
            var result=await serviceProduct.Services.GetProductGetId(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result=await serviceProduct.Services.GetAllBrandAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceProduct.Services.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
