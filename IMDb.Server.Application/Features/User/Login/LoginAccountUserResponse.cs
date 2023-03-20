namespace IMDb.Server.Application.Features.User.Login;

public record LoginAccountUserResponse(
    bool IsActive,
    string Token,
    string RefreshToken
);