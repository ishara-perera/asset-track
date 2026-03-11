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
        if (assetResponse.IsSuccess) return Ok(assetResponse);
        if (assetResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(assetResponse);
        return assetResponse.StatusCode == HttpStatusCode.BadRequest ? BadRequest(assetResponse) : StatusCode((int)assetResponse.StatusCode, assetResponse);

    }
    

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Asset>> GetAssetById(int id)
    // {
    //     var assetResponse = await assetService.GetAssetByIdAsync(id);
    //     if (assetResponse.IsSuccess) return Ok(assetResponse);
    //     return assetResponse.StatusCode switch
    //     {
    //         HttpStatusCode.NotFound => NotFound(assetResponse),
    //         HttpStatusCode.BadRequest => BadRequest(assetResponse),
    //         _ => StatusCode((int)assetResponse.StatusCode, assetResponse)
    //     };
    // }

    [HttpPost]
    public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
    {
        var assetResponse = await assetService.CreateAsync(asset);
        if (assetResponse.IsSuccess) return Ok(assetResponse);
        return assetResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(assetResponse),
            HttpStatusCode.BadRequest => BadRequest(assetResponse),
            _ => StatusCode((int)assetResponse.StatusCode, assetResponse)
        };
    }

    // [HttpPut]
    // public async Task<ActionResult<Asset>> UpdateAsset(Asset asset)
    // {
    //     var assetResponse = await assetService.UpdateAssetAsync(asset);
    //     if (assetResponse.IsSuccess) return NoContent();
    //     return assetResponse.StatusCode switch
    //     {
    //         HttpStatusCode.NotFound => NotFound(assetResponse),
    //         HttpStatusCode.BadRequest => BadRequest(assetResponse),
    //         _ => StatusCode((int)assetResponse.StatusCode, assetResponse)
    //     };
    // }
    

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteAsset(int id)
    // {
    //     var assetResponse = await assetService.DeleteAssetAsync(id);
    //     if (assetResponse.IsSuccess) return NoContent();
    //     return assetResponse.StatusCode switch
    //     {
    //         HttpStatusCode.NotFound => NotFound(assetResponse),
    //         HttpStatusCode.BadRequest => BadRequest(assetResponse),
    //         _ => StatusCode((int)assetResponse.StatusCode, assetResponse)
    //     };
    // }
}