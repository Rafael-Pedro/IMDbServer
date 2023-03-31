namespace IMDb.Server.Application.Features.Adm.Account.Login;

public record LoginAccountAdmCommandResponse(
    string Token,
    string RefreshToken
);
