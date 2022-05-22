using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Wond.Auth.Dtos;
using Wond.Auth.Models;

namespace Wond.Auth.Services;

public class AuthService : IAuthService {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly ITokenService _token;

    public AuthService(
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole<long>> roleManager,
           ITokenService token
           ) {
        _userManager = userManager;
        _roleManager = roleManager;
        _token = token;
    }

    public async Task<SessionDto?> Login(LoginDto model) {
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

            return new SessionDto(
                UserId: user.Id,
                UserName: user.UserName,
                Token: new JwtSecurityTokenHandler().WriteToken(token),
                Expiration: token.ValidTo,
                RefreshToken: user.RefreshToken,
                RefreshExpiration: (DateTime)user.RefreshTokenExpiration
            );
        }
        return null;
    }

    public async Task Register(RegisterDto model) {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            throw new Exception("User already exists!");


        ApplicationUser user = new() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new Exception("User creation failed! Please check user details and try again.");


        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));

        await _userManager.AddToRoleAsync(user, UserRoles.User);

    }

    public async Task RegisterAdmin(RegisterDto model) {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            throw new Exception("User already exists!");

        ApplicationUser user = new() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new Exception("User creation failed! Please check user details and try again.");

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
    }

    public async Task<SessionDto?> RefreshSession(RefreshTokenDto en) {

        var principal = _token.GetPrincipalFromExpiredToken(en.ExpiredToken!);
        var userId = principal.Claims.First(x => x.Type == new IdentityOptions().ClaimsIdentity.UserIdClaimType).Value;
        var userName = principal.Identity!.Name;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.RefreshToken != en.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now) {
            throw new Exception("Invalid client request");
        }

        var newAccessToken = _token.GenerateAccessToken(principal.Claims.ToList());
        var newRefreshToken = _token.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiration = _token.GetNewRefreshExpiration();
        await _userManager.UpdateAsync(user);

        return new SessionDto(
            UserId: user.Id,
            UserName: user.UserName,
            Token: new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            Expiration: newAccessToken.ValidTo,
            RefreshToken: user.RefreshToken,
            RefreshExpiration: (DateTime)user.RefreshTokenExpiration
        );
    }
}
