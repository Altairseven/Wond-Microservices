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
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly ITokenService _token;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            ITokenService token
            ) {
            _userManager = userManager;
            _roleManager = roleManager;
            _token = token;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model) {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(new IdentityOptions().ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles) {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _token.GenerateAccessToken(authClaims);


                user.RefreshToken = _token.GenerateRefreshToken();
                user.RefreshTokenExpiration = _token.GetNewRefreshExpiration();
                await _userManager.UpdateAsync(user);

                return Ok(new {
                    userId = user.Id,
                    userName = user.UserName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    refreshToken = user.RefreshToken,
                    refreshExpiration = user.RefreshTokenExpiration
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new() {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));

            await _userManager.AddToRoleAsync(user, UserRoles.User);


            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model) {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new() {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "Admin User created successfully!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> ObtainRefreshToken(RefreshTokenDto en) {
            if (en.RefreshToken == null || en.ExpiredToken == null)
                return BadRequest();

            var principal = _token.GetPrincipalFromExpiredToken(en.ExpiredToken);
            var userId = principal.Claims.First(x => x.Type == new IdentityOptions().ClaimsIdentity.UserIdClaimType).Value;
            var userName = principal.Identity!.Name;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != en.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now) {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _token.GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = _token.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = _token.GetNewRefreshExpiration();
            await _userManager.UpdateAsync(user);


            return Ok(new {
                userId = user.Id,
                userName = user.UserName,
                token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                expiration = newAccessToken.ValidTo,
                refreshToken = user.RefreshToken,
                refreshExpiration = user.RefreshTokenExpiration
            });
        }

        
    }
}

