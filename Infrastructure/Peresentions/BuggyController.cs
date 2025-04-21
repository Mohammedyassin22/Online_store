using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions
{
    [ApiController]
    [Route("api/[controller]")]

    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }


        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }


        [HttpGet(template:"badrequest")]
        public IActionResult GetBAdRequest()
        {
            return BadRequest();
        }


        [HttpGet(template: "badrequest/{id}")]
        public IActionResult GetBAdRequest(int id)
        {
            return BadRequest();
        }


        [HttpGet(template: "unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
