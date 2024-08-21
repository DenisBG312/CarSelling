using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        public string? ImgUrl { get; set; }
    }
}
