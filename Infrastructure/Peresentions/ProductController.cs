using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peresentions.Attributes;
using Services;
using ServicesAbstractions;
using Shared;
using Shared.ErrorModels;
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
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(PaginationResponse<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache(100)]
        public  async Task <ActionResult<PaginationResponse<ProductDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameter specparams)

        {
            var result = await serviceProduct.Services.GetAllProductsAsync( specparams);
            return Ok(result);
        }


        [HttpGet("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task <ActionResult<ProductDto>>GetProductById(int id)
        {
            var result=await serviceProduct.Services.GetProductGetId(id);
            return Ok(result);
        }


        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var result=await serviceProduct.Services.GetAllBrandAsync();
            return Ok(result);
        }


        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var result = await serviceProduct.Services.GetAllTypesAsync();
            return Ok(result);
        }
    }
}
