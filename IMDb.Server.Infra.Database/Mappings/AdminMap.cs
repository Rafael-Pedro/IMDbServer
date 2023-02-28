using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class AdminMap : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(344);
        builder.Property(u => u.IsActive).IsRequired();
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(32);
        builder.Property(u => u.PasswordHashSalt).IsRequired().HasMaxLength(10);
    }
}
