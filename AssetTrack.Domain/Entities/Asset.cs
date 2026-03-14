using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AssetTrack.Domain.Entities;

namespace AssetTrack.API.Models;

public sealed class Asset
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string SerialNumber { get; set; } = string.Empty;

    public DateTime PurchaseDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PurchasePrice { get; set; }

    public int? EmployeeId { get; set; }

    [JsonIgnore]
    public Employee? AssignedEmployee { get; set; }
}