namespace CarSelling.Models
{
    public class CarIndexViewModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal? Price { get; set; }
        public string ImgUrl { get; set; } = null!;
    }
}
