using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Casts.EditCast;

public record EditCastCommand(
    int Id,
    string Name,
    string Description,
    DateTime? BirthDate
) : IRequest<Result>;
