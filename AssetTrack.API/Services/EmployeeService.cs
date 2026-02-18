using System.Net;
using AssetTrack.API.Controllers;
using AssetTrack.API.Models;
using AssetTrack.API.Repository;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public class EmployeeService(IEmployeeRepository repository, ILogger<EmployeeController> logger) : IEmployeeService
{
    public async Task<ResponseInfo<List<Employee?>>> GetAllEmployeeAsync()
    {
        var employees = await repository.GetEmployeeAllAsync();
        return ResponseInfo<List<Employee?>>.Success(employees, HttpStatusCode.OK, "Return employee data");
    }

    public async Task<ResponseInfo<Employee?>> GetEmployeeByIdAsync(int id)
    {
        var employee = await repository.GetEmployeeByIdAsync(id);
        return employee != null
            ? ResponseInfo<Employee?>.Success(employee, HttpStatusCode.OK, "Return employee data")
            : ResponseInfo<Employee?>.Failure("No employee information found", HttpStatusCode.NotFound);
    }

    public async Task<ResponseInfo<Employee>> CreateEmployeeAsync(Employee employee)
    {
        try
        {
            var newEmployee = await repository.CreateEmployeeAsync(employee);
            return ResponseInfo<Employee>.Success(newEmployee , HttpStatusCode.Created,"Employee created successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while processing the employee creation. Exception {Message}", ex.Message);
            return ResponseInfo<Employee>.Failure($"An error occured while processing the employee creation. {ex.Message}",
                HttpStatusCode.BadRequest);
        }
        
    }
        
    public async Task<ResponseInfo<bool>> UpdateEmployeeAsync(int id, Employee employee)
    {
        try
        {
            var updatedEmployee = repository.UpdateEmployeeAsync(id, employee);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await repository.DeleteEmployeeAsync(id);
    }
}