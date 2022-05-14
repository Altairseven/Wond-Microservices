using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wond.Shared.Dtos;
using Wond.Shared.MessageBus.Client;

namespace Wond.Sells.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        private readonly IMessageBusClient _bus;
        private readonly IDistributedCache _dc;

        public DefaultController(IMessageBusClient bus, IDistributedCache dc) {
            _bus = bus;
            _dc = dc;
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

        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> testRedist()
        {
            var textitoCacheado = await _dc.GetStringAsync("fiscal");
            if (textitoCacheado != null)
                return Ok($"{textitoCacheado} from Cache");

            await Task.Delay(3000);

            var textitoACachear = "muerto";
            await _dc.SetStringAsync("fiscal", textitoACachear);
            

            return Ok($"{textitoACachear} not from cache");
        }
    }
}
