using IMDb.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Server.Infra.Database.Mappings;

public class VoteMap : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Rate).IsRequired().HasMaxLength(4);
        builder.HasOne(v => v.User).WithMany(u => u.Votes).HasForeignKey(v => v.UserId);
        builder.HasOne(v => v.Movie).WithMany(m => m.Votes).HasForeignKey(v => v.MovieId);
    }
}
