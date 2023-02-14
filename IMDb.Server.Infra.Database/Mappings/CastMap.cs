using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class CastMap : IEntityTypeConfiguration<Cast>
{
    public void Configure(EntityTypeBuilder<Cast> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(400);
        builder.Property(c => c.DateBirth).IsRequired();
        builder.HasMany(c => c.ActedMovies).WithOne(cm => cm.Cast).HasForeignKey(cm => cm.CastId);
        builder.HasMany(c => c.DirectedMovies).WithOne(cm => cm.Cast).HasForeignKey(cm => cm.CastId);

    }
}
