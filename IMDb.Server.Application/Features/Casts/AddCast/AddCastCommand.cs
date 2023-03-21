using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Casts.AddCast;

public record CastCommand(
    string Name,
    string Description,
    DateTime BirthDate
) : IRequest<Result<CastCommandHandlerResponse>>;