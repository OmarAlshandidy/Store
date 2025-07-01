using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servies.Abstractions;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class PaymentsController(IServiceManger serviceManger):ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId) 
        {
            var result = await serviceManger.paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);  
        }
    }
}
