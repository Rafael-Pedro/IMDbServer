using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMDb.Server.Infra.Database;

public class IMDbContext : DbContext
{
    public IMDbContext(DbContextOptions<IMDbContext> options) : base(options)
    {
    }

    public DbSet<Cast> Casts { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<GenresMovies> GenresMovies { get; set; }
    public DbSet<Movies> Movies { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
}
