using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        [StringLength(500, MinimumLength = 2)]
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
