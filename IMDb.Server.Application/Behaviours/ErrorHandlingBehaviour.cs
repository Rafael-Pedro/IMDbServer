using FluentResults;
using IMDb.Server.Application.Extension;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IMDb.Server.Application.Behaviours;

public class ErrorHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger;

    public ErrorHandlingBehaviour(ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UnKnow Error");
            return Result.Fail(new Error("Unknow Error")).To<TResponse>();
        }
    }
}