using System.Net;
using AssetTrack.API.Models;
using AssetTrack.API.Repository;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public class AssetService(IAssetRepository repository, ILogger<AssetService> logger) : IAssetService
{

    public async Task<ResponseInfo<List<Asset>>> GetAllAsync()
    {
        var assets = await repository.GetAllAsync();
        return ResponseInfo<List<Asset>>.Success(assets, HttpStatusCode.OK, "Return asset data");
    }

    public async Task<ResponseInfo<Asset?>> GetAssetByIdAsync(int id)
    {
        var asset = await repository.GetAssetByIdAsync(id);
        return asset != null
            ? ResponseInfo<Asset?>.Success(asset, HttpStatusCode.OK, "Return asset data")
            : ResponseInfo<Asset?>.Failure("No asset information found", HttpStatusCode.NotFound);
    }

    public async Task<ResponseInfo<Asset>> CreateAsync(Asset asset)
    {
        try
        {
            var newAsset = await repository.CreateAsync(asset);
            return ResponseInfo<Asset>.Success(newAsset , HttpStatusCode.Created,"Asset created successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while processing the asset creation. Exception {Message}", ex.Message);
            return ResponseInfo<Asset>.Failure($"An error occured while processing the asset creation. {ex.Message}",
                HttpStatusCode.BadRequest);
        }
    }
        
    public async Task<ResponseInfo<bool>> UpdateAssetAsync(Asset asset)
    {
        try
        { 
            var existingAsset = await repository.GetAssetByIdAsync(asset.Id);
            if (existingAsset == null)
                return ResponseInfo<bool>.Failure("asset not found", HttpStatusCode.BadRequest);
            var response = await repository.UpdateAssetAsync(asset);
            return !response ? ResponseInfo<bool>.Failure("Could not update asset", HttpStatusCode.NotModified) : ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "asset updated");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while processing the asset update. Exception {Message}", ex.Message);
            return ResponseInfo<bool>.Failure($"An error occured while updating the asset. {ex.Message}",
                HttpStatusCode.BadRequest);
        }
    }

    public async Task<ResponseInfo<bool>> DeleteAssetAsync(int id)
    {
        try
        {
            var response = await repository.DeleteAssetAsync(id);
            if (!response)
                return ResponseInfo<bool>.Failure("Asset not found", HttpStatusCode.BadRequest);
            return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "asset updated");

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while processing the asset update. Exception {Message}", ex.Message);
            return ResponseInfo<bool>.Failure($"An error occured while updating the asset. {ex.Message}",
                HttpStatusCode.BadRequest); 
        }
    }
}