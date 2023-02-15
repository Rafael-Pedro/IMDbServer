using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class CastMoviesDirectMap : IEntityTypeConfiguration<CastDirectMovies>
{
    public void Configure(EntityTypeBuilder<CastDirectMovies> builder)
    {
        builder.HasKey(cdm => cdm.Id);
        builder.HasOne(cdm => cdm.CastDirector).WithMany(c => c.DirectedMovies).HasForeignKey(cdm => cdm.CastDirectorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(cdm => cdm.MoviesDirect).WithMany(m => m.DirectorCast).HasForeignKey(cdm => cdm.MoviesDirectId).OnDelete(DeleteBehavior.Restrict);
    }
}
