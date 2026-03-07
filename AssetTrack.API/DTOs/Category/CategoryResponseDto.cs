using AssetTrack.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public CategoryResponseDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
        Description = category.Description;
    }
}
