using MediatR;
using FluentResults;
using IMDb.Server.Application.UserInfo;

namespace IMDb.Server.Application.Behaviours;

public class SetUserInfoBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>, IUserId
{
    private readonly IUserInfo userInfo;

    public SetUserInfoBehaviour(IUserInfo userInfo)
    {
        this.userInfo = userInfo;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        request.UserId = userInfo.Id;

        return await next();
    }
}
