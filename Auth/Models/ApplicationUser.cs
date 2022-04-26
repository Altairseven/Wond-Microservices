using Microsoft.AspNetCore.Identity;

namespace Wond.Auth.Models;

public class ApplicationUser: IdentityUser<long> {

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }

    public ApplicationUser() {
    }

    public ApplicationUser(string userName) {
        SecurityStamp = Guid.NewGuid().ToString();
        UserName = userName;
    }

    public ApplicationUser(string userName, string refreshToken,DateTime refreshTokenExpiration) {
        SecurityStamp = Guid.NewGuid().ToString();
        UserName = userName;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
    }

}
