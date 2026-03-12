using AssetTrack.API.DTOs;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public interface IAssetService
{
  Task<ResponseInfo<IEnumerable<AssetDto>>> GetAllAsync();

    // Task<ResponseInfo<Asset?>> GetAssetByIdAsync(int id);

    Task<ResponseInfo<AssetDto>> CreateAsync(AssetDto assetDto);

    // Task<ResponseInfo<bool>> UpdateAssetAsync(Asset asset);

    // Task<ResponseInfo<bool>>  DeleteAssetAsync(int id);
}