using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Database;

public class AppDbContext : DbContext
{
    public DbSet<Ride> Rides { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNSTRING");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ride>().HasNoKey();
    }
}
