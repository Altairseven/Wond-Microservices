using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Wond.Auth.Controllers {
    [Route("auth")]
    [ApiController]
    public class DefaultController : ControllerBase {

        private readonly ILogger<DefaultController> _logger;


        public DefaultController(ILogger<DefaultController> logger) {
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetOk() { 
            return Ok("Ok From Auth Service");
        }

        [HttpGet]
        [Authorize]
        [Route("testauth")]
        public IActionResult TestAuth() {
            return Ok("Ok From Auth Service (Authorized)");
        }

        [HttpGet]
        [Route("testerror")]
        public IActionResult GetmaybeError() {

            try {
                var rnd = new Random();
                if (rnd.Next(0, 5) < 2) {
                    throw new Exception("Forced Error");
                }



                return Ok("Ok From Auth Service");
            }
            catch (Exception ex) {
                
                _logger.LogError(ex, "something nanied wrongly");
                return new StatusCodeResult(500);
                
            }

            
        }
    }
}
