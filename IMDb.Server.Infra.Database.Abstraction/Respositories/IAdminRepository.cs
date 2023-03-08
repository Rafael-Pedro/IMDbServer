using IMDb.Server.Domain.Entities;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IAdminRepository
{
    Task Create(Admin admin, CancellationToken cancellationToken);
    void Update(Admin admin);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
    Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken);
    Task<Admin?> GetByUserName(string username, CancellationToken cancellationToken);
    Task<Admin?> GetByEmail(string email, CancellationToken cancellationToken);
}
