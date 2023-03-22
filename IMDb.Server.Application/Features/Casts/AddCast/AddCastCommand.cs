using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Casts.AddCast;

public record AddCastCommand(
    string Name,
    string Description,
    DateTime BirthDate
) : IRequest<Result<CastCommandResponse>>;