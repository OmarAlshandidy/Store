using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Servies.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet] // Get : api/basket?id=sas
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManger.basketService.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost] // Post: api/baskets
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        
        {
            var result = await serviceManger.basketService.UpdateBasketAsync(basketDto);  
            return Ok(result);
        }
        [HttpDelete] //Delete : api/basket?id
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await serviceManger.basketService.DeleteBasketAsync(id);
            return NoContent(); //204
        }



    }
}
