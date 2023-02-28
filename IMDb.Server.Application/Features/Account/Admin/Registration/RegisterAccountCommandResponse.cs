namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public record RegisterAccountCommandResponse(bool IsActive, string Email, string Username);
