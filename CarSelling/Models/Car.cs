using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using CarSelling.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace CarSelling.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Model { get; set; } = null!;
        [Required]
        [Range(0, double.MaxValue)]
        public double Mileage { get; set; }
        [Range(typeof(decimal), "0", "20000000")]
        public decimal? Price { get; set; }
        [Required]
        public int CarCreationYear { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        [Required]
        public int NumberOfDoors { get; set; }
        [Required]
        public string Location { get; set; } = null!;

        [StringLength(255)]
        public string? SafetyFeatures { get; set; }
        [StringLength(255)]
        public string? ComfortFeatures { get; set; }
        [Required]
        public EngineType EngineType { get; set; }
        public ColorEnum? Color { get; set; }
        
    }
}
