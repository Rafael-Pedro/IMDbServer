using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class GenresRepository : IGenresRepository
{
    private readonly IMDbContext context;

    public GenresRepository(IMDbContext context)
      => this.context = context;
    public async Task Create(Genres genres, CancellationToken cancellationToken)
        => await context.Genres.AddAsync(genres, cancellationToken);
    public void Update(Genres genres)
        => context.Genres.Update(genres);
    public void Delete(Genres genres)
        => context.Genres.Remove(genres);
    public IEnumerable<Genres> GetAll()
        => context.Genres;
    public Task<bool> IsAlreadyRegistered(IEnumerable<int> id, CancellationToken cancellationToken)
        => context.Genres.AnyAsync(g => id.Contains(g.Id), cancellationToken);

    public async Task<bool> IsUniqueGenre(string name, CancellationToken cancellationToken)
       => await context.Genres.AnyAsync(n => n.Name.ToLower() == name.ToLower(), cancellationToken);

}
