using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet("notfound")] // Get : Api/Buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            //code

            return NotFound(); //404
        }
        [HttpGet("servererror")]  // Get : api/Buggy /servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")]  // Get : api/Buggy /badrequest
        public IActionResult GetbadRequest()
        {
            return BadRequest(); //400
        }
        [HttpGet("badrequest/{id}")]  // Get : api/Buggy /badrequest/id
        public IActionResult GetbadRequest(int id ) // Validation Error
        {
            //Code
            return BadRequest(); //400
        }

        [HttpGet("unauthorized")]  // Get : api/Buggy /unauthorized
        public IActionResult GetUnauthorizedRequest()
        {
            //Code
            return Unauthorized(); //401
        }


    }
}
