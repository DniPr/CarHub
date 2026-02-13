using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarHub.GCommon.EntityValidations.EntityValidations;
namespace CarHub.Models
{
    public class CarAd
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CarTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(CarModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = CarPriceType)]
        public decimal Price { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        [MaxLength(CarFuelTypeMaxLength)]
        public string FuelType { get; set; } = null!;

        [Required]
        [MaxLength(CarTransmissionMaxLength)]
        public string Transmission { get; set; } = null!;

        [Required]
        [MaxLength(CarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;
        public virtual IdentityUser Owner { get; set; } = null!;

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
