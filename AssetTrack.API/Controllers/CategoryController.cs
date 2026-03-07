using System.Net;
using AssetTrack.API.Models;
using AssetTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(
    ICategoryService categoryService,
    ILogger<CategoryController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        logger.LogInformation("Call the API service for get all asset information");
        var assetResponse = await categoryService.GetAllAsync();
        if (assetResponse.IsSuccess)
            return Ok(assetResponse);
        if (assetResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(assetResponse);
        return assetResponse.StatusCode == HttpStatusCode.BadRequest
            ? BadRequest(assetResponse)
            : StatusCode((int)assetResponse.StatusCode, assetResponse);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetAssetById(int id)
    {
        var assetResponse = await categoryService.GetCategoryByIdAsync(id);
        if (assetResponse.IsSuccess)
            return Ok(assetResponse);
        return assetResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(assetResponse),
            HttpStatusCode.BadRequest => BadRequest(assetResponse),
            _ => StatusCode((int)assetResponse.StatusCode, assetResponse),
        };
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> CreateAsset(
        CreateCategoryDto createCategoryDto
    )
    {
        var response = await categoryService.CreateAsync(createCategoryDto);
        if (response.IsSuccess)
            return Ok(response);
        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.BadRequest => BadRequest(response),
            _ => StatusCode((int)response.StatusCode, response),
        };
    }

    [HttpPut]
    public async Task<ActionResult<CategoryResponseDto>> UpdateAsset(
        UpdateCategoryDto updateCategoryDto
    )
    {
        var response = await categoryService.UpdateCategoryAsync(updateCategoryDto);
        if (response.IsSuccess)
            return NoContent();
        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(response),
            HttpStatusCode.BadRequest => BadRequest(response),
            _ => StatusCode((int)response.StatusCode, response),
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(int id)
    {
        var assetResponse = await categoryService.DeleteCategoryAsync(id);
        if (assetResponse.IsSuccess)
            return NoContent();
        return assetResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(assetResponse),
            HttpStatusCode.BadRequest => BadRequest(assetResponse),
            _ => StatusCode((int)assetResponse.StatusCode, assetResponse),
        };
    }
}
