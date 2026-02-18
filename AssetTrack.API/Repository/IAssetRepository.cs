using AssetTrack.API.Models;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Repository 
{
    public interface IAssetRepository 
    {
        Task<List<Asset>> GetAllAsync();

        Task<Asset?> GetAssetByIdAsync(int id);

        Task<Asset> CreateAsync(Asset asset);

        Task<bool> UpdateAssetAsync(Asset asset);

        Task<bool> DeleteAssetAsync(int id);
    }
}