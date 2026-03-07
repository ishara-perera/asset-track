using AssetTrack.API.Models;

namespace AssetTrack.API.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Asset> Assets { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Category> Categories { get; set; }
}
