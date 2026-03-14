using AssetTrack.Application.DTOs;
using AssetTrack.Application.Wrapper;
using AssetTrack.Domain.Entities;

namespace AssetTrack.Application.Interfaces;

public interface IEmployeeService
{
    Task<ResponseInfo<IEnumerable<EmployeeDto>>> GetAllEmployeeAsync();

    // Task<ResponseInfo<Employee?>> GetEmployeeByIdAsync(int id);

    Task<ResponseInfo<Employee>> CreateEmployeeAsync(EmployeeDto employeeDto);

    // Task<ResponseInfo<bool>> UpdateEmployeeAsync(Employee employee);
    
    // Task<ResponseInfo<bool>> DeleteEmployeeAsync(int id);
}