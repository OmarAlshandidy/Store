using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Servies.Abstractions;
using Shared;
using Shared.ErrorModels;

namespace Presentation
{
    // Api Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManger serviceManger):ControllerBase  
    {
        // EndPoint : public non-static method

        // Sorting : nameasc [defult ]
        // sort : namedes
        // sort : priceDesc
        // sor :  priceasc


        [HttpGet] // Get : /Api/products
        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cash(100)]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters specParams)
        {
            var result = await serviceManger.productService.GetAllProductAsync(specParams);
            return Ok(result); // 200 
        }

        [HttpGet("{id}")] // Get:  /api/products/12
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<ProductResultDto>>  GetProductByIdAsyn(int id)
        {
           var resalt = await serviceManger.productService.GetProductAsync(id);
            return Ok(resalt);
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<BrandResultDto>> GetAllBrands()   // Get : /api/products/brands
        {
               var result = await serviceManger.productService.GetAllBrandsAsync();
            if(result == null) return BadRequest();//400
            return Ok(result); //200
        }
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async  Task<ActionResult<TypeResultDto>> GetAllTypes() // Get : /api/product/types
        {
            var result = await serviceManger.productService.GetAllTypesAsync();
            if(result == null) return BadRequest(); // 400
            return Ok(result); //  200
        }
    }
}
