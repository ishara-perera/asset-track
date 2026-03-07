using AssetTrack.API.Models;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public interface ICategoryService
{
    Task<ResponseInfo<List<Category>>> GetAllAsync();

    Task<ResponseInfo<Category?>> GetCategoryByIdAsync(int id);

    Task<ResponseInfo<CategoryResponseDto>> CreateAsync(CreateCategoryDto categoryDto);

    Task<ResponseInfo<bool>> UpdateCategoryAsync(UpdateCategoryDto categoryDto);

    Task<ResponseInfo<bool>> DeleteCategoryAsync(int id);
}
