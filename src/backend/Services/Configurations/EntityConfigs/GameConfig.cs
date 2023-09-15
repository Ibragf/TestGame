using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Entities;

namespace Services.Configurations.EntityConfigs;

public sealed class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");

        builder
            .Property(g => g.State)
            .HasConversion<string>()
            .IsConcurrencyToken();
    }
}