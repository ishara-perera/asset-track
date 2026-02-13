using AssetTrack.API.Data;
using AssetTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTrack.API.Repository;

public class AssetRepository(AppDbContext context) : IAssetRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<Asset>> GetAllAsync()
    {
        return await _context.Assets.ToListAsync();
    }

    public async Task<Asset?> GetAssetByIdAsync(int id)
    {
        return await _context.Assets.FindAsync(id);
        
    }

    public async Task<Asset> CreateAsync(Asset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        return asset;
    }

    public async Task<Asset?> UpdateAssetAsync(int id, Asset asset)
    {
        _context.Entry(asset).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Assets.AnyAsync(e => e.Id == id))
            {
                return null; 
            }
            else
            {
                throw; 
            }
        }

        return asset;
    }    
    public async Task DeleteAssetAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset != null) _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();
    }
}