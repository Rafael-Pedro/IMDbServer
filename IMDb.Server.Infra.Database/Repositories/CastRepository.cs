using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

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

    public Task<Cast?> GetById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Cast> GetAll()
    => context.Casts;
    public Task<bool> IsAlreadyRegistred(IEnumerable<int> id, CancellationToken cancellationToken)
    => context.Casts.AnyAsync(g => id.Contains(g.Id), cancellationToken);

    public async Task<bool> IsUniqueCast(string name, CancellationToken cancellationToken)
    => await context.Casts.AnyAsync(n => n.Name.ToLower() == name.ToLower(), cancellationToken);

    public void Update(Cast cast)
    => context.Update(cast);

}
