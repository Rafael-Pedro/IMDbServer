using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.Account.Login;

public record LoginAccountAdmCommand(string Username, string Password) : IRequest<Result<LoginAccountAdmCommandResponse>>;