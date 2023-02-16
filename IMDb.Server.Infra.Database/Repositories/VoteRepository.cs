using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class VoteRepository : IVoteRepository
{
    public Task<Vote> Create(Vote vote, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(Vote vote)
    {
        throw new NotImplementedException();
    }

    public void Update(Vote vote)
    {
        throw new NotImplementedException();
    }
}
