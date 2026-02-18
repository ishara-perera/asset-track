using AssetTrack.API.Models;
using AssetTrack.API.Wrapper;

namespace AssetTrack.API.Services;

public interface IEmployeeService
{
    Task<ResponseInfo<List<Employee>>> GetAllEmployeeAsync();

    Task<ResponseInfo<Employee?>> GetEmployeeByIdAsync(int id);

    Task<ResponseInfo<Employee>> CreateEmployeeAsync(Employee employee);

    Task<ResponseInfo<bool>> UpdateEmployeeAsync(Employee employee);

    Task<ResponseInfo<bool>> DeleteEmployeeAsync(int id);
}