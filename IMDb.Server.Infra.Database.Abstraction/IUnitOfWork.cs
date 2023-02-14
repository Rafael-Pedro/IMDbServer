namespace IMDb.Server.Infra.Database.Abstraction;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
