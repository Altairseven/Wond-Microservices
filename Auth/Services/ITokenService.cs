using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Wond.Auth.Services;

public interface ITokenService {
    JwtSecurityToken GenerateAccessToken(List<Claim> authClaims);
    string GenerateRefreshToken();
    DateTime GetNewRefreshExpiration();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}