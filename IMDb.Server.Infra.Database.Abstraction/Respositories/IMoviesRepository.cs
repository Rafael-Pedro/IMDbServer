using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IMoviesRepository
{
    Task<Movies> Create(Movies movies, CancellationToken cancellationToken);
    void Update(Movies movies);
    void Delete(Movies movies);
    Task<Movies> GetById(int id, CancellationToken cancellationToken);
    IEnumerable<Movies> GetAll(PaginatedQueryOptions paginatedQueryOptions);
    IEnumerable<Movies> GetByGenre(IEnumerable<Genres> genres, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken);
    IEnumerable<Movies> GetByActors(IEnumerable<Cast> actors, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken);
    IEnumerable<Movies> GetByDirector(IEnumerable<Cast> directors, PaginatedQueryOptions paginatedQueryOptions, CancellationToken cancellationToken);
}
