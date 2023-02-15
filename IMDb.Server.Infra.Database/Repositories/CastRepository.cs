using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class CastRepository : ICastRepository
{
    public Task Create(Cast cast, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(Cast cast)
    {
        throw new NotImplementedException();
    }

    public Task<Cast?> GetByBirthDate(string BirhDate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Cast?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CastActMovies?> GetByMovie(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Cast?> GetByName(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAlreadyRegistered(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(Cast cast)
    {
        throw new NotImplementedException();
    }
}
