using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private readonly IMDbContext context;

    public MoviesRepository(IMDbContext context)
    => this.context = context;

    public async Task Create(Movies movies, CancellationToken cancellationToken)
    => await context.Movies.AddAsync(movies, cancellationToken);
    public void Delete(Movies movies)
    => context.Remove(movies);
    public void Update(Movies movies)
    => context.Update(movies);
    public async Task<Movies?> GetById(int id, CancellationToken cancellationToken)
    => await context.Movies.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    public async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
    => await context.Movies.AnyAsync(m => m.Name == name, cancellationToken) is false;
    public IEnumerable<Movies> GetAll(PaginatedQueryOptions paginatedQueryOptions)
    => throw new NotImplementedException();

}
