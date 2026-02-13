using System.ComponentModel.DataAnnotations;
using static CarHub.GCommon.EntityValidations.EntityValidations;
namespace CarHub.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<CarAd> CarAds { get; set; } = new List<CarAd>();
    }
}
