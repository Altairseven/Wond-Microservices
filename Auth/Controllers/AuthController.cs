using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wond.Auth.Dtos;
using Wond.Auth.Models;
using Wond.Auth.Services;

namespace Wond.Auth.Controllers {
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) {
            this._auth = auth;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) {
            var session = await _auth.Login(model);
            if (session != null)
                return Ok(session);

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model) {
            try {
                await _auth.Register(model);

                return Ok(new ResponseDto {
                    Status = "Success",
                    Message = "User created successfully!"
                });

            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model) {
            try {
                await _auth.RegisterAdmin(model);

                return Ok(new ResponseDto {
                    Status = "Success",
                    Message = "Admin User created successfully!"
                });

            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshSession(RefreshTokenDto en) {
            try {
                return Ok(await _auth.RefreshSession(en));
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { Status = "Error", Message = ex.Message });
            }


        }


    }
}

