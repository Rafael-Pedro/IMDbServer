using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Casts.RemoveCast;

public record RemoveCastCommand(int Id) : IRequest<Result>;
