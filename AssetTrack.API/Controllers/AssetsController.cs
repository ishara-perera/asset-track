using AssetTrack.API.Models;
using AssetTrack.API.Repositories;
using AssetTrack.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IAssetsRepository _repository;

    public AssetsController(IAssetsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Asset>>> GetAll()
    {
        var assets = await _repository.GetAllAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAssetById(int id)
    {
        var asset = await _repository.GetAssetByIdAsync(id);
        if (asset == null)
            return NotFound();

        return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
    }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        var newAsset = _repository.CreateAsync(asset);
        return CreatedAtAction(nameof(GetAssetById), new { Id = newAsset.Id }, newAsset);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
    {
        if (id != asset.Id)
            return BadRequest();

        var updatedAsset = _repository.UpdateAssetAsync(id, asset);

        if (updatedAsset == null)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        await _repository.DeleteAssetAsync(id);
        return NoContent();
    }
}