using System.ComponentModel.DataAnnotations;
using AssetTrack.API.Models;

namespace AssetTrack.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public List<Asset> Assets { get; init; } = [];
    }
}