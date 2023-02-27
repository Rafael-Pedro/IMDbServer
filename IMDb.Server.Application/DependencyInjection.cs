using IMDb.Server.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.Server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationHandlingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SetUserInfoBehaviour<,>));



        return services;
    }
}
