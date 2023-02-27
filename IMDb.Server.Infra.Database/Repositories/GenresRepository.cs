using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class GenresRepository : IGenresRepository
{
    public Task<Genres> Create(Genres genres, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(Genres genres)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Genres> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Genres genres)
    {
        throw new NotImplementedException();
    }
}
