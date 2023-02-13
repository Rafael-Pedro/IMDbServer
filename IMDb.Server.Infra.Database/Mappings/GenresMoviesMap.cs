using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class GenresMoviesMap : IEntityTypeConfiguration<GenresMovies>
{
    public void Configure(EntityTypeBuilder<GenresMovies> builder)
    {
        builder.HasKey(gm => gm.Id);
        builder.HasOne(gm => gm.Genres).WithMany(g => g.GenresMovies).HasForeignKey(gm => gm.Id);
        builder.HasOne(gm=>gm.Movies).WithMany(m => m.GenresMovies).HasForeignKey(gm => gm.Id);
    }
}
