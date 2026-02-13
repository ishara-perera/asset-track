using AssetTrack.API.Controllers;
using AssetTrack.API.Models;
using AssetTrack.API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Services;

public class AssetsService(IAssetsRepository repository, ILogger<AssetsController> logger) : IAssetsService
{
    private readonly IAssetsRepository _repository = repository;
    private readonly ILogger _logger = logger;
    
    public async Task<List<Asset>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Asset?> GetAssetByIdAsync(int id)
    {
        return await _repository.GetAssetByIdAsync(id);
    }

    public async Task<Asset> CreateAsync(Asset asset)
    {
        return await _repository.CreateAsync(asset);
    }
        
    public async Task<Asset?> UpdateAssetAsync(int id, Asset asset)
    {
        return await _repository.UpdateAssetAsync(id, asset);
    }

    public async Task DeleteAssetAsync(int id)
    {
        await _repository.DeleteAssetAsync(id);
    }
}