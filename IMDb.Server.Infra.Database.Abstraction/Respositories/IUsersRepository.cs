using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IUsersRepository
{
    Task Create(Users users, CancellationToken cancellationToken);
    void Delete(Users users);
    void Update(Users users);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
}
