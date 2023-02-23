using FluentResults;
using IMDb.Server.Application.UserInfo;
using MediatR;

namespace IMDb.Server.Application.Behaviours;

public class SetUserInfoBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ResultBase, new()
    where TResponse : IRequest<TRequest>
{
    private readonly IUserInfo userInfo;

    public SetUserInfoBehavior(IUserInfo userInfo)
    {
        this.userInfo = userInfo;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
