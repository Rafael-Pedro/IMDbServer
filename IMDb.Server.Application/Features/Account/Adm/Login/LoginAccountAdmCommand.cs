using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public record LoginAccountAdmCommand(string Username, string Password): IRequest<Result<LoginAccountAdmCommandResponse>>
{
    public int UserId { get; set; }
}