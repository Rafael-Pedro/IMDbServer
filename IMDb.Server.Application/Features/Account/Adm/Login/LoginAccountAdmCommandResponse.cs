namespace IMDb.Server.Application.Features.Account.Adm.Login;

public record LoginAccountAdmCommandResponse(
    bool IsActive,
    string Token ,
    string RefreshToken
);
