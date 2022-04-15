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
    }
}
