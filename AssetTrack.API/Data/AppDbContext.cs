using AssetTrack.API.Models;
namespace AssetTrack.API.Data;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Employee> Employees { get; set; }
}