using System.Net;
using AssetTrack.API.Models;
using AssetTrack.Application.DTOs;
using AssetTrack.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AssetController(IAssetService assetService, ILogger<EmployeeController> logger) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles="Admin,User")]
    public async Task<ActionResult<List<AssetDto>>> GetAll()
    {
        logger.LogInformation("Call the API service for get all asset information");
        
        var assetResponse = await assetService.GetAllAsync();
        if (assetResponse.IsSuccess) return Ok(assetResponse);
        if (assetResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(assetResponse);
        return assetResponse.StatusCode == HttpStatusCode.BadRequest ? BadRequest(assetResponse) : StatusCode((int)assetResponse.StatusCode, assetResponse);

    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetDto>> GetAssetById(int id)
    {
        var assetResponse = await assetService.GetAssetByIdAsync(id);
        if (assetResponse.IsSuccess) return Ok(assetResponse);
        return assetResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(assetResponse),
            HttpStatusCode.BadRequest => BadRequest(assetResponse),
            _ => StatusCode((int)assetResponse.StatusCode, assetResponse)
        };
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Asset>> CreateAsset(AssetDto assetDto)
    {
        logger.LogInformation("Call the API service for get all asset information");

        var assetResponse = await assetService.CreateAsync(assetDto);
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