using AssetTrack.API.Models;
using AssetTrack.API.Repositories;
using AssetTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController(IAssetsService assetsService, ILogger<AssetsController> logger) : ControllerBase
{
    private readonly IAssetsService _assetsService = assetsService;
    private readonly ILogger<AssetsController> _logger = logger;
    
    [HttpGet]
    public async Task<ActionResult<List<Asset>>> GetAll()
    {
        var assets = await _assetsService.GetAllAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAssetById(int id)
    {
        var asset = await _assetsService.GetAssetByIdAsync(id);
        if (asset == null)
            return NotFound();

        return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
    }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        _logger.LogInformation("Calling create asset API service...");
        var newAsset = await _assetsService.CreateAsync(asset);
        return CreatedAtAction(nameof(GetAssetById), new { newAsset.Id }, newAsset);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
    {
        if (id != asset.Id)
            return BadRequest();

        var updatedAsset = await _assetsService.UpdateAssetAsync(id, asset);

        if (updatedAsset == null)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        await _assetsService.DeleteAssetAsync(id);
        return NoContent();
    }
}