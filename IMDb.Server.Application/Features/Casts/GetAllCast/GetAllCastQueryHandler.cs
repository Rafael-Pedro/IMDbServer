using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.Casts.GetAllCast;

public class GetAllCastQueryHandler : IRequestHandler<GetAllCastQuery, Result<GetAllCastQueryResponse>>
{

    public Task<Result<GetAllCastQueryResponse>> Handle(GetAllCastQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
