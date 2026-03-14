using AssetTrack.API.Models;
using AssetTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetTrack.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // This is essential for Identity to set up its own internal tables!
        base.OnModelCreating(builder);

        // Seed Roles directly into the database schema
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole 
            { 
                Id = "1", 
                Name = "Admin", 
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString() 
            },
            new IdentityRole 
            { 
                Id = "2", 
                Name = "User", 
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString() 
            }
        );
    }
}