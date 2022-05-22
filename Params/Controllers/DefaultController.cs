using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wond.Params.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetOk() {
            return Ok("Ok From Params Service");
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public IActionResult TestAuth() {
            return Ok("Ok From Params Service (Authorized)");
        }
    }
}
