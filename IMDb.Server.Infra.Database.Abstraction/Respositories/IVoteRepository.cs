using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IVoteRepository
{
    public Task<Vote> Create(Vote vote, CancellationToken cancellationToken);
    void Update(Vote vote);
    void Delete(Vote vote);
}
