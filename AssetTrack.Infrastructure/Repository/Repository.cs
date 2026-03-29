using AssetTrack.Application.Interfaces;
using AssetTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetTrack.Infrastructure.Repository;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddEntity(T entity)
    {
        _dbSet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}