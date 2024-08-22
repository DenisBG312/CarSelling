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
    }
}
