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
        // 1. Fetch the existing entity (EF tracks this)
        var existingAsset = await _context.Assets.FindAsync(id);

        if (existingAsset == null)
        {
            return null; // Not found
        }

        // 2. Copy values from the input 'asset' to the 'existingAsset'
        // This updates the tracked entity safely.
        _context.Entry(existingAsset).CurrentValues.SetValues(asset);

        // 3. Save changes
        await _context.SaveChangesAsync();

        return existingAsset;
    }  
    public async Task<Asset?> DeleteAssetAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return null;
        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();
        return asset;
    }
}