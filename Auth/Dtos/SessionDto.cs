namespace Wond.Auth.Dtos;

public record SessionDto(
    long UserId, 
    string UserName, 
    string Token, 
    DateTime Expiration, 
    string RefreshToken, 
    DateTime RefreshExpiration
);