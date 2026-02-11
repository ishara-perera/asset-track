using AssetTrack.API.Data;
using AssetTrack.API.Models;
using AssetTrack.API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DefaultNamespace;

public class AssetsRepository : IAssetsRepository
{
    private readonly AppDbContext _context;

    public AssetsRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<Asset>> GetAllAsync()
    {
        return await _context.Assets.ToListAsync();
    }

    public async Task<Asset?> GetAssetByIdAsync(int id)
    {
        return await _context.Assets.FindAsync(id);    }

    public async Task<Asset> CreateAsync(Asset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        return asset;
    }

    public async Task<Asset?> UpdateAssetAsync(int id, Asset asset)
    {
        if (id != asset.Id)
        {
            return null;
        }

        _context.Entry(asset).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assets.Any(e => e.Id == id))
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

    public Task DeleteAssetAsync(Type id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAssetAsync(int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset != null) _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();
    }
}