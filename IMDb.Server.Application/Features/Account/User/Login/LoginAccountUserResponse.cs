namespace IMDb.Server.Application.Features.Account.User.Login;

public record LoginAccountUserResponse(
    bool IsActive,
    string Token,
    string RefreshToken
);