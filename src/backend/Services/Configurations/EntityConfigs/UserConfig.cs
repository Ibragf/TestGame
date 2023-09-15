using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Entities;

namespace Services.Configurations.EntityConfigs;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder
            .HasMany(u => u.Games)
            .WithOne(g => g.User)
            .HasForeignKey(g => g.UserId);
    }
}