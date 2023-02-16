using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using MulviParking.Server.Infra.Database.Abstractions;

namespace IMDb.Server.Infra.Database.Repositories;

public class MoviesRepository : IMoviesRepository
{
    public Task<Movies> Create(Movies movies, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(Movies movies)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Movies> GetAll(PaginatedQueryOptions paginatedQueryOptions)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Movies> GetByActors(IEnumerable<Cast> actors, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Movies> GetByDirector(IEnumerable<Cast> directors, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Movies> GetByGenre(IEnumerable<Genres> genres, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Movies> GetById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(Movies movies)
    {
        throw new NotImplementedException();
    }
}
