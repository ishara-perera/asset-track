using System.Net;
using AssetTrack.API.Models;
using AssetTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController(IAssetService assetService, ILogger<EmployeeController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Asset>>> GetAll()
    {
        logger.LogInformation("Call the API service for get all asset information");
        var assetResponse = await assetService.GetAllAsync();
        if (!assetResponse.IsSuccess)
        {
            if (assetResponse.StatusCode == HttpStatusCode.NotFound)
                return NotFound(assetResponse);
            if (assetResponse.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(assetResponse);


            return StatusCode((int)assetResponse.StatusCode, assetResponse);
        }

        return Ok(assetResponse);
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetAssetById(int id)
    {
        var assetResponse = await assetService.GetAssetByIdAsync(id);
        if (!assetResponse.IsSuccess)
        {
            if (assetResponse.StatusCode == HttpStatusCode.NotFound)
                return NotFound(assetResponse);
            if (assetResponse.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(assetResponse);

            return StatusCode((int)assetResponse.StatusCode, assetResponse);
        }

        return Ok(assetResponse);

    }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        var assetResponse = await assetService.CreateAsync(asset);
        if (!assetResponse.IsSuccess)
        {
            if (assetResponse.StatusCode == HttpStatusCode.NotFound)
                return NotFound(assetResponse);
            if (assetResponse.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(assetResponse);

            return StatusCode((int)assetResponse.StatusCode, assetResponse);
        }

        return Ok(assetResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Asset>> UpdateAsset(int id, Asset asset)
    {
        var assetResponse = await assetService.UpdateAssetAsync(id, asset);
        if (!assetResponse.IsSuccess)
        {
            if (assetResponse.StatusCode == HttpStatusCode.NotFound)
                return NotFound(assetResponse);
            if (assetResponse.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(assetResponse);

            return StatusCode((int)assetResponse.StatusCode, assetResponse);
        }

        return NoContent();
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        await assetService.DeleteAssetAsync(id);
        return NoContent();
    }
}