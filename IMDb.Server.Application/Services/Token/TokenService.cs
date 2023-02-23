using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MulviParking.Server.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MulviParking.Server.Application.Services.Token;

public class TokenService : ITokenService
{
    private readonly byte[] key;
    private readonly IEnumerable<TokenValidationParameters> tokenValidations;

    public TokenService(IOptions<TokenServiceOptions> options, IEnumerable<TokenValidationParameters> tokenValidations)
    {
        key = options.Value.Key;
        this.tokenValidations = tokenValidations.Select(tv =>
        {
            tv.IssuerSigningKey = new SymmetricSecurityKey(key); 
            return tv;
        });
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "User")
            }),
            Issuer = "https://mulviparking.com",
            Audience = "MulviParking",
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public string GenerateToken(int id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Role, "Requester")
            }),
            Issuer = "https://mulviparking.com",
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,"ConfirmEmail")
            }),
            Issuer = "https://mulviparking.com",
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = "https://mulviparking.com",
            Audience = "MulviParkingRt",
            Expires = DateTime.UtcNow.AddDays(30),
            TokenType = "rt+jwt",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsValidToken(string token)
        => new JwtSecurityTokenHandler()
        .ValidateTokenAsync(token, tokenValidations.Last()).GetAwaiter().GetResult().IsValid;

    public bool IsValidRefreshToken(string refreshtoken)
        => new JwtSecurityTokenHandler().ValidateTokenAsync(refreshtoken, tokenValidations.First()).GetAwaiter().GetResult().IsValid;

    public int TokenInfo(string token)
    {
       var tokenInfo = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidations.Last(), out var securityToken);
       return Convert.ToInt32(tokenInfo.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
    }
}

