using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.User.Login;

public record LoginAccountUserCommand(string Username, string Password) : IRequest<Result<LoginAccountUserResponse>>;