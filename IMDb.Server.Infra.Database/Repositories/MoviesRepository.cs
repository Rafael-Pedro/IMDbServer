using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private readonly IMDbContext context;

    public MoviesRepository(IMDbContext context)
    {
        this.context = context;
    }

    public Task<Movies> Create(Movies movies, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public void Delete(Movies movies)
    {
        throw new NotImplementedException();
    }
    public void Update(Movies movies)
    {
        throw new NotImplementedException();
    }
    public Task<Movies> GetById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
    => await context.Movies.AnyAsync(m => m.Name == name, cancellationToken) is false;
    public IEnumerable<Movies> GetAll(PaginatedQueryOptions paginatedQueryOptions)
    {
        throw new NotImplementedException();
    }
}

