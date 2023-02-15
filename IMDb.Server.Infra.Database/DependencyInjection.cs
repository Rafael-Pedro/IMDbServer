using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMDb.Server.Infra.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("IMDbServerConnection");
        services.AddDbContext<IMDbContext>(opts => opts.UseSqlServer(connectionString));
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVoteRepository, VoteRepository>();
        services.AddScoped<ICastRepository, CastRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IMoviesRepository, MoviesRepository>();
        services.AddScoped<IGenresRepository, GenresRepository>();
        services.AddScoped<IGenresMoviesRepository, GenresMoviesRepository>();

        return services;
    }
}
