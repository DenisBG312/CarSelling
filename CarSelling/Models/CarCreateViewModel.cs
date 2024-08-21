using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarCreateViewModel
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Model { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public double Mileage { get; set; }

        public string? ImgUrl { get; set; }

        // List of brands for the dropdown
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
    }
}
