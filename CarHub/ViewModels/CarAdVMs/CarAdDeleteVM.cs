namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdDeleteVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
