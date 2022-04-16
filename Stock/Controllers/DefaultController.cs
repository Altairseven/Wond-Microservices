using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wond.Stock.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        [HttpGet]
        public IActionResult GetOk() {
            return Ok("Ok From Stock Service");
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public IActionResult TestAuth() {
            return Ok("Ok From Stock Service (Authorized)");
        }
    }
}
