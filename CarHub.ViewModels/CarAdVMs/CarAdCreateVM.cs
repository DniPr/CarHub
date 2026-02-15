using System.ComponentModel.DataAnnotations;
using static CarHub.GCommon.EntityValidations.EntityValidations;

namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdCreateVM
    {
        [Required]
        [MinLength(CarTitleMinLength)]
        [MaxLength(CarTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(CarBrandMinLength)]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [MinLength(CarModelMinLength)]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; } = null!;

        [Range(1990, 2026)]
        public int Year { get; set; }

        [Range(typeof(decimal), "0", "10000000")]
        public decimal Price { get; set; }

        [Range(0, 2000000)]
        public int Mileage { get; set; }

        [Required]
        public string FuelType { get; set; } = null!;

        [Required]
        public string Transmission { get; set; } = null!;

        [Required]
        [MinLength(CarDescriptionMinLength)]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public virtual IEnumerable<CategoryDropdownVM> Categories { get; set; }
            = new List<CategoryDropdownVM>();
    }
}
