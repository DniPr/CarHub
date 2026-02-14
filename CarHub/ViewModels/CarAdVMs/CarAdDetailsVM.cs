namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = null!;
        public string Transmission { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public bool IsOwner { get; set; }
        public bool IsFavourite { get; set; }
    }
}
