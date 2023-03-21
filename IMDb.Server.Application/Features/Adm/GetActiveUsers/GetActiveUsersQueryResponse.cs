namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public record GetActiveUsersQueryResponse(
    int Id,
    string Username,
    string Email
);