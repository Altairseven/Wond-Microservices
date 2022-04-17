using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wond.Shared.Dtos;
using Wond.Shared.MessageBus.Client;

namespace Wond.Sells.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        private readonly IMessageBusClient _bus;

        public DefaultController(IMessageBusClient bus) {
            _bus = bus;
        }

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
            _bus.SendMessage("texto", "A Nisman lo mataron");
            _bus.SendMessage("color", new ProductColor(1, "Amarillo"));

            return Ok("Ok From Sells Service (Authorized)");
        }
    }
}
