using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IMDbContext context;

    public AdminRepository(IMDbContext context)
    {
        this.context = context;
    }

    public async Task Create(Admin admin, CancellationToken cancellationToken)
    => await context.AddAsync(admin, cancellationToken);

    public void Update(Admin admin)
    => context.Update(admin);
    public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    => await context.Admins.AnyAsync(a => a.Email == email, cancellationToken) is false;

    public async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
    => await context.Admins.AnyAsync(a => a.Username == username, cancellationToken) is false;

    public async Task<Admin?> GetByUserName(string username, CancellationToken cancellationToken)
    => await context.Admins.FirstOrDefaultAsync(a => a.Username == username, cancellationToken);

    public async Task<Admin?> GetByEmail(string email, CancellationToken cancellationToken)
    => await context.Admins.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);

    public async Task<Admin?> GetById(int id, CancellationToken cancellationToken)
        => await context.Admins.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
}
