using AssetTrack.API.Models;

namespace AssetTrack.API.Repository 
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeeAllAsync();   

        Task<Employee?> GetEmployeeByIdAsync(int id);

        Task<Employee> CreateEmployeeAsync(Employee employee);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}