using FluentResults;
using FluentValidation;
using IMDb.Server.Application.Extension;
using MediatR;

namespace IMDb.Server.Application.Behaviours;

public class ValidationHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validator;

    public ValidationHandlingBehaviour(IEnumerable<IValidator<TRequest>> validator)
    {
        this.validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var result = await Task.WhenAll(validator.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errorMessageDictionary = result.SelectMany(r => r.Errors)
            .GroupBy(e => e.PropertyName)
            .ToDictionary(eg => eg.Key, eg => eg.Select(e => e.ErrorMessage).ToArray());

        if (errorMessageDictionary.Count != 0)
            return Result.Fail(new ValidationError(errorMessageDictionary)).To<TResponse>();

        return await next();
    }
}
