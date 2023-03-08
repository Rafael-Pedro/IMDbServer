namespace IMDb.Server.Application.Features.Account.Adm.Login;

public record LoginAccountCommandResponse(
    bool IsActive,
    string Token ,
    string RefreshToken
);
