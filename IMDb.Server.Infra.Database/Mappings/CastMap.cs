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
        builder.HasMany(c => c.ActedMovies).WithOne(cm => cm.CastAct).HasForeignKey(cm => cm.CastActId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.DirectedMovies).WithOne(cm => cm.CastDirector).HasForeignKey(cm => cm.CastDirectorId).OnDelete(DeleteBehavior.Restrict);
    }
}
