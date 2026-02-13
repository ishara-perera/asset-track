using AssetTrack.API.Models;
using AssetTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController(IAssetService assetService, ILogger<EmployeeController> logger) : ControllerBase
{
    private readonly IAssetService _assetService = assetService;
    private readonly ILogger<EmployeeController> _logger = logger;
    
    [HttpGet]
    public async Task<ActionResult<List<Asset>>> GetAll()
    {
        var assets = await _assetService.GetAllAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAssetById(int id)
    {
        var asset = await _assetService.GetAssetByIdAsync(id);
        if (asset == null)
            return NotFound();

        return Ok(asset);    }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        _logger.LogInformation("Calling create asset API service...");
        var newAsset = await _assetService.CreateAsync(asset);
        return CreatedAtAction(nameof(GetAssetById), new { newAsset.Id }, newAsset);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
    {
        if (id != asset.Id)
            return BadRequest();

        var updatedAsset = await _assetService.UpdateAssetAsync(id, asset);

        if (updatedAsset == null)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        await _assetService.DeleteAssetAsync(id);
        return NoContent();
    }
}