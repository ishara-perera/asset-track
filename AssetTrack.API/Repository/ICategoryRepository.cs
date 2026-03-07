using AssetTrack.API.Models;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetCategoryByIdAsync(int id);

        Task<Category> CreateAsync(Category category);

        Task<bool> UpdateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
