using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wond.Sells.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        [HttpGet]
        public IActionResult GetOk() {
            return Ok("Ok From Sells Service");
        }


        [HttpGet]
        [Authorize]
        [Route("test")]
        public IActionResult TestAuth() {
            return Ok("Ok From Sells Service (Authorized)");
        }

        [HttpGet]
      
        [Route("send")]
        public IActionResult testRabbit() {
            return Ok("Ok From Sells Service (Authorized)");
        }
    }
}
