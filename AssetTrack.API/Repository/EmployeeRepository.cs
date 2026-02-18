using AssetTrack.API.Data;
using AssetTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTrack.API.Repository;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<List<Employee>> GetEmployeeAllAsync()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await context.Employees.FindAsync(id);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null) return false;
        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return true;
    }
}