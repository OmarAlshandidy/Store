using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Servies.Abstractions;

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
        public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId,string? sort , int pageIndex = 1 , int pageSize=5)
        {
            var result = await serviceManger.productService.GetAllProductAsync(brandId , typeId,sort, pageIndex, pageSize);
            if (result == null) return BadRequest(); //400
            return Ok(result); // 200 
        }

        [HttpGet("{id}")] // Get:  /api/products/12
        public async Task<IActionResult>  GetProductByIdAsyn(int id)
        {
           var resalt = await serviceManger.productService.GetProductAsync(id);
            if(resalt is null ) return NotFound();    // 404
            return Ok(resalt);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()   // Get : /api/products/brands
        {
               var result = await serviceManger.productService.GetAllBrandsAsync();
            if(result == null) return BadRequest();//400
            return Ok(result); //200
        }
        [HttpGet("types")]
        public async  Task<IActionResult> GetAllTypes() // Get : /api/product/types
        {
            var result = await serviceManger.productService.GetAllTypesAsync();
            if(result == null) return BadRequest(); // 400
            return Ok(result); //  200
        }
    }
}
