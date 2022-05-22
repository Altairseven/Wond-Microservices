using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wond.Params.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase {





}
