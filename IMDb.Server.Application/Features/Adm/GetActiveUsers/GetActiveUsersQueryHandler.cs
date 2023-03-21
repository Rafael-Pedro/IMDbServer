using MediatR;
using FluentResults;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Abstraction;

namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public class GetActiveUsersQueryHandler : IRequestHandler<GetActiveUsersQuery, Result<IEnumerable<GetActiveUsersQueryResponse>>>
{
    private readonly IUsersRepository usersRepository;

    public GetActiveUsersQueryHandler(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public Task<Result<IEnumerable<GetActiveUsersQueryResponse>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
    {
        var page = new PaginatedQueryOptions(request.Page, request.PageSize, request.IsDescending);

        var usersActive = usersRepository.GetAllActiveUsers(page);

        var result = usersActive.Select(ua => new GetActiveUsersQueryResponse(ua.Id, ua.Username, ua.Email));

        return Task.FromResult(Result.Ok(result));
    }
}
