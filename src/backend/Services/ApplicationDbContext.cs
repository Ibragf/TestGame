using Microsoft.EntityFrameworkCore;
using Services.Entities;

namespace Services;
    
public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Game> Games => Set<Game>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}