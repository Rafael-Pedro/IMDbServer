using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public record LoginAccountCommand(string Username, string Password): IRequest<Result<LoginAccountCommandResponse>>
{
    public int UserId { get; set; }
}