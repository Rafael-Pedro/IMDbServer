using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IMoviesRepository
{
    Task<Movies> Create(Movies movies, CancellationToken cancellationToken);
    void Update(Movies movies);
    void Delete(Movies movies);
    Task<Movies> GetById(int id, CancellationToken cancellationToken);
    Task<bool> IsUniqueName(string name, CancellationToken cancellationToken);
    IEnumerable<Movies> GetAll(PaginatedQueryOptions paginatedQueryOptions);
}
