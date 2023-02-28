using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Application.Services.Token;

public interface ITokenService
{
    string? GenerateToken(Admin admin);
    string? GenerateToken(Users user);
    string GenerateRefreshToken();

}
