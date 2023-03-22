using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Casts.GetAllCast;

public record GetAllCastQuery() : IRequest<Result<GetAllCastQueryResponse>>;