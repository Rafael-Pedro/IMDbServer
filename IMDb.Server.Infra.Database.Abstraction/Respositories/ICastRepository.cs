using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface ICastRepository
{
    Task Create(Cast cast, CancellationToken cancellationToken);
    void Delete(Cast cast);
    void Update(Cast cast);
    Task<Cast?> GetById(int id, CancellationToken cancellationToken);
    IEnumerable<Cast> GetAll(PaginatedQueryOptions paginatedQuery);
    Task<bool> IsAlreadyRegistred(IEnumerable<int> id, CancellationToken cancellationToken);
    Task<bool> IsUniqueCast(string name, CancellationToken cancellationToken);
}
