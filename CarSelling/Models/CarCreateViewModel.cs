using CarSelling.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarCreateViewModel
    {
        [Required]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Полето за модел е задължително.")]
        [StringLength(150, MinimumLength = 2)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "Въведете реален пробег.")]
        [Range(0, double.MaxValue)]
        public double Mileage { get; set; }
        [Range(typeof(decimal), "0", "20000000")]
        [Required(ErrorMessage = "Полето за цена е задължително.")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int CarCreationYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Полето за снимка е задължително.")]
        public string ImgUrl { get; set; } = null!;
        [Required(ErrorMessage = "Полето за брой на врати е задължително.")]
        public int NumberOfDoors { get; set; }
        [Required(ErrorMessage = "Полето за локация е задължително.")]
        public string Location { get; set; } = null!;

        [StringLength(255)]
        public string? SafetyFeatures { get; set; }
        [StringLength(255)]
        public string? ComfortFeatures { get; set; }
        [Required]
        public EngineType EngineType { get; set; }
        public ColorEnum? Color { get; set; }

        // List of brands for the dropdown
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
    }
}
