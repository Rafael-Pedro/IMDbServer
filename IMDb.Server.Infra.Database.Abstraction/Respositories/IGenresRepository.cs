using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;
public interface IGenresRepository
{
    Task Create(Genres genres, CancellationToken cancellationToken);
    void Update(Genres genres);
    void Delete(Genres genres);
    IEnumerable<Genres> GetAll();
    Task<bool> IsAlreadyRegistered(IEnumerable<int> id, CancellationToken cancellationToken);
    Task<bool> IsUniqueGenre(string name, CancellationToken cancellationToken);
}
