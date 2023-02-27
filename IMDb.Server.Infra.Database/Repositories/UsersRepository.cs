using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMDbContext context;

    public UsersRepository(IMDbContext context)
    {
        this.context = context;
    }

    public async Task Create(Users users, CancellationToken cancellationToken)
        => await context.Users.AddAsync(users, cancellationToken);

    public void Delete(Users users)
       => context.Remove(users);

    public Task<Users?> GetAllActiveUsers(PaginatedQueryOptions paginatedQueryOptions)
        => throw new NotImplementedException();

    public async Task<Users?> GetByEmail(string email, CancellationToken cancellationToken)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<Users?> GetByName(string name, CancellationToken cancellationToken)
        => await context.Users.FirstOrDefaultAsync(u => u.Username == name, cancellationToken);

    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        => !await context.Users.AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
        => !await context.Users.AnyAsync(u => u.Username == username, cancellationToken);

    public void Update(Users users)
        => context.Update(users);

}
