using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IGenresMoviesRepository
{
    Task<GenresMovies> Create(GenresMovies genresMovies, CancellationToken cancellationToken);
    void Delete(GenresMovies genresMovies);
}
