using FluentResults;
using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Application.Services.Token;

public interface ITokenService
{
    string? GenerateToken(Account account);
    string GenerateRefreshToken();
}
