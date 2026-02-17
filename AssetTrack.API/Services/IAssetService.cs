using AssetTrack.API.Models;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public interface IAssetService
{
    Task<ResponseInfo<List<Asset>>> GetAllAsync();

    Task<ResponseInfo<Asset>> GetAssetByIdAsync(int id);

    Task<ResponseInfo<Asset>> CreateAsync(Asset asset);

    Task<ResponseInfo<bool>> UpdateAssetAsync(int id, Asset asset);

    Task<ResponseInfo<bool>>  DeleteAssetAsync(int id);
}