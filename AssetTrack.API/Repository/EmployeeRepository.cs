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

    public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
    {
        var existingEmployee = await context.Employees.FindAsync(id);
        if (existingEmployee == null) return null;
        context.Entry(employee).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> DeleteEmployeeAsync(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null) return null;
        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return employee;
    }
}