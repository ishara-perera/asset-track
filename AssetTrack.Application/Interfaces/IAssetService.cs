using AssetTrack.Application.DTOs;
using AssetTrack.Application.Wrapper;

namespace AssetTrack.Application.Interfaces;

public interface IAssetService
{
  Task<ResponseInfo<IEnumerable<AssetDto>>> GetAllAsync();

    Task<ResponseInfo<AssetDto?>> GetAssetByIdAsync(int id);

    Task<ResponseInfo<AssetDto>> CreateAsync(AssetDto assetDto);

    // Task<ResponseInfo<bool>> UpdateAssetAsync(Asset asset);

    // Task<ResponseInfo<bool>>  DeleteAssetAsync(int id);
}