using AssetTrack.API.Data;
using AssetTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTrack.API.Repository;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<Employee>> GetEmployeeAllAsync()
    {
        return await _context.Employees.ToListAsync();

    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);  
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
        
    }

    public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Employees.AnyAsync(e => e.Id == id))
            {
                return null; 
            }
            else
            {
                throw; 
            }
        }

        return employee;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)  _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}