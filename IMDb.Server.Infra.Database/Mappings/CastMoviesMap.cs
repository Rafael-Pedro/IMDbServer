using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class CastMoviesMap : IEntityTypeConfiguration<CastMovies>
{
    public void Configure(EntityTypeBuilder<CastMovies> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.HasOne(cm => cm.Cast).WithMany(c => c.ActedMovies).HasForeignKey(cm => cm.CastId);
        builder.HasOne(cm => cm.Cast).WithMany(c => c.DirectedMovies).HasForeignKey(cm => cm.CastId);
        builder.HasOne(cm => cm.Movies).WithMany(m => m.ActorCast).HasForeignKey(cm => cm.MoviesId);
        builder.HasOne(cm => cm.Movies).WithMany(m => m.DirectorCast).HasForeignKey(cm => cm.MoviesId);
    }
}
