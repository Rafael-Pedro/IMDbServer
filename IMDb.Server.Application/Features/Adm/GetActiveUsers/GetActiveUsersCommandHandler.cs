using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.Adm.GetActiveUsers;

public class GetActiveUsersCommandHandler : IRequestHandler<GetActiveUsersCommand, Result>
{
    Task<Result> IRequestHandler<GetActiveUsersCommand, Result>.Handle(GetActiveUsersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
