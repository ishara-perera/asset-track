using AssetTrack.API.Models;
using AssetTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly ILogger<EmployeeController> _logger = logger;
    
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var assets = await _employeeService.GetAllEmployeeAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var asset = await _employeeService.GetEmployeeByIdAsync(id);
        if (asset == null)
            return NotFound();

        return CreatedAtAction(nameof(GetEmployeeById), new { id = asset.Id }, asset);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateAsset(Employee employee)
    {
        _logger.LogInformation("Calling create asset API service...");
        var newAsset = await _employeeService.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { newAsset.Id }, newAsset);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateAsset(int id, Employee employee)
    {
        if (id != employee.Id)
            return BadRequest();

        var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employee);

        if (updatedEmployee == null)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return NoContent();
    }
}