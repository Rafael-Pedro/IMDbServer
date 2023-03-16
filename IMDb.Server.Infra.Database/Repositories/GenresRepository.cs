using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class GenresRepository : IGenresRepository
{
    private readonly IMDbContext context;

    public GenresRepository(IMDbContext context)
    {
        this.context = context;
    }

    public Task<Genres> Create(Genres genres, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public void Delete(Genres genres)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Genres> GetAll()
    => context.Genres;
    public Task<bool> ExistingGenders(IEnumerable<int> id, CancellationToken cancellationToken)
    => context.Genres.AnyAsync(g => id.Contains(g.Id), cancellationToken);
    public void Update(Genres genres)
    {
        throw new NotImplementedException();
    }
}
