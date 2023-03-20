namespace IMDb.Server.Application.Features.Adm.Account.Login;

public record LoginAccountAdmCommandResponse(
    bool IsActive,
    string Token,
    string RefreshToken
);
