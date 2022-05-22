using Wond.Auth.Dtos;
using Wond.Auth.Models;

namespace Wond.Auth.Services;

public interface IAuthService {
    Task<SessionDto?> Login(LoginDto model);
    Task<SessionDto?> RefreshSession(RefreshTokenDto en);
    Task Register(RegisterDto model);
    Task RegisterAdmin(RegisterDto model);
}