using AssetTrack.API.Models;

namespace AssetTrack.API.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeeAsync();

    Task<Employee?> GetEmployeeByIdAsync(int id);

    Task<Employee> CreateEmployeeAsync(Employee employee);

    Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);

    Task DeleteEmployeeAsync(int id);
}