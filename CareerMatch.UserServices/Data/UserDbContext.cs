namespace CareerMatch.UserServices.Data;

using Microsoft.EntityFrameworkCore;
using CareerMatch.UserServices.Models; // Reference your models namespace

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // Optional: Override OnModelCreating to configure mappings
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.DateOfBirth)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
                v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified)); // Ensure unspecified kind on retrieval

        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
    }
}
