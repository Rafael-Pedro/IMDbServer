using FluentResults;
using IMDb.Server.Application.Features.Adm.GetActiveUsers;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MediatR;

namespace IMDb.Server.Application.Features.Casts.GetAllCast;

public class GetAllCastQueryHandler : IRequestHandler<GetAllCastQuery, Result<IEnumerable<GetAllCastQueryResponse>>>
{
    private readonly ICastRepository castRepository;

    public GetAllCastQueryHandler(ICastRepository castRepository)
    {
        this.castRepository = castRepository;
    }

    public Task<Result<IEnumerable<GetAllCastQueryResponse>>> Handle(GetAllCastQuery request, CancellationToken cancellationToken)
    {
        var page = new PaginatedQueryOptions(request.Page, request.PageSize, request.IsDescending);

        var castList = castRepository.GetAll(page);

        if (castList is null)
            return Task.FromResult(Result.Ok(Enumerable.Empty<GetAllCastQueryResponse>()));

        return Task.FromResult(Result.Ok(castList.Select(c => new GetAllCastQueryResponse(c.Id, c.Name, c.Description, c.DateBirth))));
    }
}
