using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public record RegisterAccountCommand(string Login, string Email, string Password) : IRequest<Result<RegisterAccountCommandResponse>>, IUserId
{
    public int UserId { get; set; }
}
