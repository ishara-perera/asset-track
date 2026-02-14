using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
   public Employee? Employee { get; set; }
}