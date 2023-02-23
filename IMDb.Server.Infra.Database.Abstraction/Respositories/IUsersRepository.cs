using IMDb.Server.Domain.Entities;
using MulviParking.Server.Infra.Database.Abstractions;

namespace IMDb.Server.Infra.Database.Abstraction.Respositories;

public interface IUsersRepository
{
    Task Create(Users users, CancellationToken cancellationToken);
    void Delete(Users users);
    void Update(Users users);
    Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
    Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken);
    Task<Users?> GetByName(string name, CancellationToken cancellationToken);
    Task<Users?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Users?> GetAllActiveUsers(PaginatedQueryOptions paginatedQueryOptions);
}
