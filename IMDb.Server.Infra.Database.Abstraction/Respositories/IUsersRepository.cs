using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IUsersRepository
{
    Task Create(Users users, CancellationToken cancellationToken);
    void Delete(Users users);
    void Update(Users users);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
    Task<bool> IsUniqueUserName(string userName, CancellationToken cancellationToken);
    Task<Users?> GetByName(string name, CancellationToken cancellationToken);
    Task<Users?> GetByEmail(string email, CancellationToken cancellationToken);
}
