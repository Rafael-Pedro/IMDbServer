using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class CastRepository : ICastRepository
{
    private readonly IMDbContext context;

    public CastRepository(IMDbContext context)
    => this.context = context;

    public async Task Create(Cast cast, CancellationToken cancellationToken)
    => await context.Casts.AddAsync(cast, cancellationToken);

    public void Delete(Cast cast)
    => context.Casts.Remove(cast);

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

    public Task<bool> IsAlreadyRegistred(IEnumerable<int> id, CancellationToken cancellationToken)
    => throw new NotImplementedException();

    public Task<bool> IsAlreadyRegistred(string name, CancellationToken cancellationToken)
    => throw new NotImplementedException();

    public void Update(Cast cast)
    => throw new NotImplementedException();
}
