using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class CastMoviesActMap : IEntityTypeConfiguration<CastActMovies>
{
    public void Configure(EntityTypeBuilder<CastActMovies> builder)
    {
        builder.HasKey(cam => cam.Id);
        builder.HasOne(cam => cam.CastAct).WithMany(c => c.ActedMovies).HasForeignKey(cam => cam.CastActId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(cam => cam.MoviesAct).WithMany(m => m.ActorCast).HasForeignKey(cam => cam.MoviesActId).OnDelete(DeleteBehavior.Restrict);
    }
}
