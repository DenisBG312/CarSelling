using CarSelling.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public double Mileage { get; set; }
        public decimal? Price { get; set; }
        public DateTime CarCreationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int NumberOfDoors { get; set; }
        public string Location { get; set; } = null!;
        public string? SafetyFeatures { get; set; }
        public string? ComfortFeatures { get; set; }
        public string EngineType { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
