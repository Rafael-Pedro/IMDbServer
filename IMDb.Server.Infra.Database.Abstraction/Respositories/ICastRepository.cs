using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface ICastRepository
{
    Task Create(Cast cast, CancellationToken cancellationToken);
    void Delete(Cast cast);
    void Update(Cast cast);
    Task<Cast?> GetById(int id);
    Task<Cast?> GetByName(string name, CancellationToken cancellationToken);
    Task<Cast?> GetByBirthDate(string BirhDate, CancellationToken cancellationToken);
    Task<CastActMovies?> GetByMovie(int id, CancellationToken cancellationToken);
    Task<bool> IsAlreadyRegistered(string name, CancellationToken cancellationToken);
}
