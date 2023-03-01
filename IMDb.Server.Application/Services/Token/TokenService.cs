using IMDb.Server.Domain.Entities;
using IMDb.Server.Domain.Entities.Abstract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMDb.Server.Application.Services.Token;

public class TokenService : ITokenService
{
    private readonly byte[] key;
    private readonly IEnumerable<TokenValidationParameters> validationParameters;

    public TokenService(IOptions<TokenService> options, IEnumerable<TokenValidationParameters> validationParameters)
    {
        key = options.Value.key;
        this.validationParameters = validationParameters.Select(tv =>
        {
            tv.IssuerSigningKey = new SymmetricSecurityKey(key);
            return tv;
        });
    }


    public string? GenerateToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.GetType().Name),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

}
