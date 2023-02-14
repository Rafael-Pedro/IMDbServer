using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;

namespace IMDb.Server.Infra.Database.Repositories;

public class UsersRepository : IUsersRepository
{
    public Task Create(Users users, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Delete(Users users)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(Users users)
    {
        throw new NotImplementedException();
    }
}
