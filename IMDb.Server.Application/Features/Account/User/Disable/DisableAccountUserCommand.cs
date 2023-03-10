using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Account.User.Disable;

public record DisableAccountUserCommand() : IRequest<Result>;
