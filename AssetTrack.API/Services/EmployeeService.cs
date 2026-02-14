using AssetTrack.API.Controllers;
using AssetTrack.API.Models;
using AssetTrack.API.Repository;

namespace AssetTrack.API.Services;

public class EmployeeService(IEmployeeRepository repository, ILogger<EmployeeController> logger) : IEmployeeService
{
    private readonly IEmployeeRepository _repository = repository;
    private readonly ILogger _logger = logger;
    
    public async Task<List<Employee>> GetAllEmployeeAsync()
    {
        return await _repository.GetEmployeeAllAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _repository.GetEmployeeByIdAsync(id);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        return await _repository.CreateEmployeeAsync(employee);
    }
        
    public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
    {
        return await _repository.UpdateEmployeeAsync(id, employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _repository.DeleteEmployeeAsync(id);
    }
}