﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wond.Auth.Controllers {
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase {

        [HttpGet]
        public IActionResult GetOk() { 
            return Ok("Ok From Auth Service");
        }
    }
}
