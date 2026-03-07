using System.Net;
using AssetTrack.API.Models;
using AssetTrack.API.Repository;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public class CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
    : ICategoryService
{
    public async Task<ResponseInfo<List<Category>>> GetAllAsync()
    {
        var categories = await repository.GetAllAsync();
        return ResponseInfo<List<Category>>.Success(
            categories,
            HttpStatusCode.OK,
            "Return categories data"
        );
    }

    public async Task<ResponseInfo<Category?>> GetCategoryByIdAsync(int id)
    {
        var category = await repository.GetCategoryByIdAsync(id);
        return category != null
            ? ResponseInfo<Category?>.Success(category, HttpStatusCode.OK, "Return category data")
            : ResponseInfo<Category?>.Failure(
                "No category information found",
                HttpStatusCode.NotFound
            );
    }

    public async Task<ResponseInfo<CategoryResponseDto>> CreateAsync(
        CreateCategoryDto createCategoryDto
    )
    {
        try
        {
            Category category = new Category();
            category.Name = createCategoryDto.Name;
            category.Description = createCategoryDto.Description;

            var newCategory = await repository.CreateAsync(category);

            return ResponseInfo<CategoryResponseDto>.Success(
                new CategoryResponseDto(category),
                HttpStatusCode.Created,
                "Category created successfully."
            );
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occured while processing the category creation. Exception {Message}",
                ex.Message
            );
            return ResponseInfo<CategoryResponseDto>.Failure(
                $"An error occured while processing the category creation. {ex.Message}",
                HttpStatusCode.BadRequest
            );
        }
    }

    public async Task<ResponseInfo<bool>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var existingCategory = await repository.GetCategoryByIdAsync(updateCategoryDto.Id);

            if (existingCategory == null)
                return ResponseInfo<bool>.Failure("category not found", HttpStatusCode.BadRequest);

            existingCategory.Name = updateCategoryDto.Name;
            existingCategory.Description = updateCategoryDto.Description;

            var response = await repository.UpdateCategoryAsync(existingCategory);
            return !response
                ? ResponseInfo<bool>.Failure(
                    "Could not update category",
                    HttpStatusCode.NotModified
                )
                : ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "asset updated");
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occured while processing the asset update. Exception {Message}",
                ex.Message
            );
            return ResponseInfo<bool>.Failure(
                $"An error occured while updating the asset. {ex.Message}",
                HttpStatusCode.BadRequest
            );
        }
    }

    public async Task<ResponseInfo<bool>> DeleteCategoryAsync(int id)
    {
        try
        {
            var response = await repository.DeleteCategoryAsync(id);
            if (!response)
                return ResponseInfo<bool>.Failure("Category not found", HttpStatusCode.BadRequest);
            return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "category updated");
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occured while processing the category update. Exception {Message}",
                ex.Message
            );
            return ResponseInfo<bool>.Failure(
                $"An error occured while updating the category. {ex.Message}",
                HttpStatusCode.BadRequest
            );
        }
    }
}
