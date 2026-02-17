using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetTrack.API.Models;

public class Asset
{
   public int Id { get; set; }

   [Required] public string Model { get; set; } = string.Empty;

   [Required] public string SerialNumber { get; set; } = string.Empty;
   
   public DateTime PurchaseDate { get; set; }
   
   [Column(TypeName = "decimal(18,2)")]
   public decimal PurchasePrice { get; set; }
   
   public int EmployeeId { get; set; }
   
   [JsonIgnore]
   public Employee? Employee { get; set; }
}