using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.User.Disable;

public record DisableAccountUserCommand() : IRequest<Result>;
