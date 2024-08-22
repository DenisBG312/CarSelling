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
        [Range(typeof(decimal), "0", "20000000")]
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public DateTime CarCreationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ImgUrl { get; set; }

        // List of brands for the dropdown
        public IEnumerable<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
    }
}
