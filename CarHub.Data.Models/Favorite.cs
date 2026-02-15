using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarHub.Data.Models
{
    public class Favorite
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public IdentityUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CarAd))]
        public int CarAdId { get; set; }

        [Required]
        public CarAd CarAd { get; set; } = null!;
    }
}
