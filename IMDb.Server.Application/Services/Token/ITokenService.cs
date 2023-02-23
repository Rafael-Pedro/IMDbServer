using MulviParking.Server.Domain.Entities;

namespace MulviParking.Server.Application.Services.Token;

public interface ITokenService
{
    string? GenerateToken(User user);
    string GenerateToken();
    string GenerateToken(int id);
    string GenerateRefreshToken();
    bool IsValidToken(string token);
    bool IsValidRefreshToken(string refreshtoken);

    int TokenInfo(string token);
    
}
