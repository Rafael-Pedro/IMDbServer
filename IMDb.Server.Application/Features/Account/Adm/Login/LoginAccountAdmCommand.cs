using FluentResults;
using IMDb.Server.Application.UserInfo;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public record LoginAccountAdmCommand(string Username, string Password): IRequest<Result<LoginAccountAdmCommandResponse>>, IUserId
{
    public int UserId { get; set; }
}