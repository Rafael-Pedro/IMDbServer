using FluentResults;
using IMDb.Server.Application.UserInfo;
using MediatR;

namespace IMDb.Server.Application.Features.Account.User.Login;

public record LoginAccountUserCommand(string Username, string Password) : IRequest<Result<LoginAccountUserResponse>>, IUserId
{
    public int UserId { get; set; }
}
