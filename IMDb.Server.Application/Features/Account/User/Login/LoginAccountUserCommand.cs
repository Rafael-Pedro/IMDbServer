using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Account.User.Login;

public record LoginAccountUserCommand(string Username, string Password) : IRequest<Result<LoginAccountUserResponse>>;