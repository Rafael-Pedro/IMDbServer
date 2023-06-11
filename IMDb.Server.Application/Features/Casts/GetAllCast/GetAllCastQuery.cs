using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Casts.GetAllCast;

public record GetAllCastQuery(
    int Page,
    int PageSize,
    bool? IsDescending) : IRequest<Result<IEnumerable<GetAllCastQueryResponse>>>;