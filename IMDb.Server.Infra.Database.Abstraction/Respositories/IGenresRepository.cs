using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;
public interface IGenresRepository
{
    public Task<Genres> Create(Genres genres, CancellationToken cancellationToken);
    void Update(Genres genres);
    void Delete(Genres genres);
    IEnumerable<Genres> GetAll();
    Task<bool> ExistingGenders(IEnumerable<int> id, CancellationToken cancellationToken);
}
