using System.Net;
using AssetTrack.Application.DTOs;
using AssetTrack.Application.Interfaces;
using AssetTrack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        logger.LogInformation("Call the API service for get all employee information");
        var employeeResponse = await employeeService.GetAllEmployeeAsync();
        if (employeeResponse.IsSuccess) return Ok(employeeResponse);
        if (employeeResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(employeeResponse);
        return employeeResponse.StatusCode == HttpStatusCode.BadRequest ? BadRequest(employeeResponse) : StatusCode((int)employeeResponse.StatusCode, employeeResponse);

    }
    

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    // {
    //     var employeeResponse = await employeeService.GetEmployeeByIdAsync(id);
    //     if (!employeeResponse.IsSuccess)
    //     {
    //         return employeeResponse.StatusCode switch
    //         {
    //             HttpStatusCode.NotFound => NotFound(employeeResponse),
    //             HttpStatusCode.BadRequest => BadRequest(employeeResponse),
    //             _ => StatusCode((int)employeeResponse.StatusCode, employeeResponse)
    //         };
    //     }
    //
    //     return Ok(employeeResponse);
    //
    // }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto employee)
    {
        var employeeResponse = await employeeService.CreateEmployeeAsync(employee);
        if (employeeResponse.IsSuccess) return Ok(employeeResponse);
        return employeeResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(employeeResponse),
            HttpStatusCode.BadRequest => BadRequest(employeeResponse),
            _ => StatusCode((int)employeeResponse.StatusCode, employeeResponse)
        };
    }

    // [HttpPut]
    // public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
    // {
    //     var employeeResponse = await employeeService.UpdateEmployeeAsync(employee);
    //     if (employeeResponse.IsSuccess) return NoContent();
    //     return employeeResponse.StatusCode switch
    //     {
    //         HttpStatusCode.NotFound => NotFound(employeeResponse),
    //         HttpStatusCode.BadRequest => BadRequest(employeeResponse),
    //         _ => StatusCode((int)employeeResponse.StatusCode, employeeResponse)
    //     };
    // }
    

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteEmployee(int id)
    // {
    //     var employeeResponse = await employeeService.DeleteEmployeeAsync(id);
    //     if (employeeResponse.IsSuccess) return NoContent();
    //     return employeeResponse.StatusCode switch
    //     {
    //         HttpStatusCode.NotFound => NotFound(employeeResponse),
    //         HttpStatusCode.BadRequest => BadRequest(employeeResponse),
    //         _ => StatusCode((int)employeeResponse.StatusCode, employeeResponse)
    //     };
    // }
}