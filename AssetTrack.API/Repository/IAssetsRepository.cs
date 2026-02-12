using AssetTrack.API.Models;

namespace AssetTrack.API.Repositories 
{
    public interface IAssetsRepository 
    {
        Task<List<Asset>> GetAllAsync();

        Task<Asset?> GetAssetByIdAsync(int id);

        Task<Asset> CreateAsync(Asset asset);

        Task<Asset?> UpdateAssetAsync(int id, Asset asset);

        Task DeleteAssetAsync(int id);
    }
}