using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class GenresMoviesRepository : IGenresMoviesRepository
{
    public Task<GenresMovies> Create(GenresMovies genresMovies, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(GenresMovies genresMovies)
    {
        throw new NotImplementedException();
    }
}
