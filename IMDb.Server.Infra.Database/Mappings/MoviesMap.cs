using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class MoviesMap : IEntityTypeConfiguration<Movies>
{
    public void Configure(EntityTypeBuilder<Movies> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Rating).IsRequired();
        builder.Property(m => m.TotalVotes).IsRequired();
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Description).IsRequired().HasMaxLength(400);
        builder.HasMany(m => m.Votes).WithOne(v => v.Movie).HasForeignKey(v => v.MovieId);
        builder.HasMany(m => m.ActorCast).WithOne(cm => cm.Movies).HasForeignKey(cm => cm.CastId);
        builder.HasMany(m => m.DirectorCast).WithOne(cm => cm.Movies).HasForeignKey(cm => cm.CastId);
    }
}
