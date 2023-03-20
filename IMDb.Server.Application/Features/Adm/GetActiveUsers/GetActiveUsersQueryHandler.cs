using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public class GetActiveUsersQueryHandler : IRequestHandler<GetActiveUsersQuery, Result>
{
    Task<Result> IRequestHandler<GetActiveUsersQuery, Result>.Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
