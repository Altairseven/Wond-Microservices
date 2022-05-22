using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Wond.Shared.Configuration;

namespace Wond.Auth.Services;

public class TokenService : ITokenService {

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims) {
        var auClaim = authClaims.FirstOrDefault(x => x.Type == "aud");
        if (auClaim != null)
            authClaims.Remove(auClaim);


        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public string GenerateRefreshToken() {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public DateTime GetNewRefreshExpiration() {
        var minutes = double.Parse(_configuration["JWT:RefreshExpirationMinutes"]);

        var time = DateTime.Now.AddMinutes(minutes).ToUniversalTime();
        return time;
    }


    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
        var tokenValidationParameters = AuthConfiguration.AppTokenValidationParameters(_configuration);
        tokenValidationParameters.ValidateLifetime = false; //asi no le importa que este expirado.
        tokenValidationParameters.LifetimeValidator = null;


        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
}
