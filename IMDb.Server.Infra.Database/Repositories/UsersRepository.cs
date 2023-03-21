using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using IMDb.Server.Infra.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMDbContext context;

    public UsersRepository(IMDbContext context)
    => this.context = context;


    public async Task Create(Users users, CancellationToken cancellationToken)
        => await context.Users.AddAsync(users, cancellationToken);

    public IEnumerable<Users> GetAllActiveUsers(PaginatedQueryOptions paginatedQueryOptions)
        => context.Users.Where(u => u.IsActive).PaginateAndOrder(paginatedQueryOptions, u => u.Username);

    public Task<Users?> GetById(int id, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<Users?> GetByEmail(string email, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public Task<Users?> GetByName(string name, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(u => u.Username == name, cancellationToken);

    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        => await context.Users.AnyAsync(u => u.Email == email, cancellationToken) is false;

    public async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
        => await context.Users.AnyAsync(u => u.Username == username, cancellationToken) is false;

    public void Update(Users users)
        => context.Update(users);
}
