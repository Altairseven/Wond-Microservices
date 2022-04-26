namespace Wond.Auth.Dtos;

public class RefreshTokenDto {
    public string? ExpiredToken { get; set; }
    public string? RefreshToken { get; set; }

    public RefreshTokenDto() {

    }

}
