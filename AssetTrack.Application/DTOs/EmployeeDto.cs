using System.ComponentModel.DataAnnotations;

namespace AssetTrack.Application.DTOs;

public sealed record EmployeeDto
{
    public int Id { get; init; }

    [Required] private string FirstName { get; init; } = string.Empty;

    [Required] private string LastName { get; init; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    public string? Department { get; init; }
    
    public int AssignedAssetCount { get; init; }
}