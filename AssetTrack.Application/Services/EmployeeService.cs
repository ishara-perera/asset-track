using System.Net;
using AssetTrack.Application.DTOs;
using AssetTrack.Application.Interfaces;
using AssetTrack.Application.Wrapper;
using AssetTrack.Domain.Entities;
using AutoMapper;

namespace AssetTrack.Application.Services;

public class EmployeeService(IRepository<Employee> repository, IMapper mapper) : IEmployeeService
{
    public async Task<ResponseInfo<IEnumerable<EmployeeDto>>> GetAllEmployeeAsync()
    {
        var result = await repository.GetAllAsync();
        var employeeDto = mapper.Map<List<EmployeeDto>>(result);
        return ResponseInfo<IEnumerable<EmployeeDto>>.Success(employeeDto, HttpStatusCode.OK, "Return employee data");
    }

    // public async Task<ResponseInfo<Employee?>> GetEmployeeByIdAsync(int id)
    // {
    //     var employee = await repository.GetEmployeeByIdAsync(id);
    //     return employee != null
    //         ? ResponseInfo<Employee?>.Success(employee, HttpStatusCode.OK, "Return employee data")
    //         : ResponseInfo<Employee?>.Failure("No employee information found", HttpStatusCode.NotFound);
    // }

    public async Task<ResponseInfo<Employee>> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);
        var response = await repository.AddEntity(employee);
        var newEmployeeDto = mapper.Map<Employee>(response);
        return ResponseInfo<Employee>.Success(newEmployeeDto, HttpStatusCode.Created,
            "Employee created successfully.");
    }

    // public async Task<ResponseInfo<bool>> UpdateEmployeeAsync(Employee employee)
    // {
    //     try
    //     {
    //         var existingEmployee = await repository.GetEmployeeByIdAsync(employee.Id);
    //         if (existingEmployee == null)
    //             ResponseInfo<bool>.Failure("Employee not found", HttpStatusCode.NotFound);
    //         return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Employee updated successfully!");
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError(ex, "An error occured while updating the employee. Exception {Message}",  ex.Message);
    //         return ResponseInfo<bool>.Failure("Employee not updated", HttpStatusCode.NotModified);
    //     }
    // }

    // public async Task<ResponseInfo<bool>> DeleteEmployeeAsync(int id)
    // {
    //     var response = await repository.DeleteEmployeeAsync(id);
    //     return !response ? ResponseInfo<bool>.Failure("Employee not found", HttpStatusCode.NotFound) : ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Employee deleted successfully!");
    // }
}
