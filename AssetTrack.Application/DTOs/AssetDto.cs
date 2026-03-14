using System.ComponentModel.DataAnnotations;

namespace AssetTrack.Application.DTOs;

public sealed record AssetDto
{
    public int Id { get; init; }

    [Required]
    public string Model { get; init; } = string.Empty;

    [Required]
    public string SerialNumber { get; init; } = string.Empty;

    public DateTime PurchaseDate { get; init; }

    public decimal PurchasePrice { get; init; }

    public int? EmployeeId { get; init; }

    public string? AssignedEmployeeName { get; init; }

    public string Status => EmployeeId.HasValue ? "Assigned" : "In Storage";
}