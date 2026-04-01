using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(CarAd))]
        public int CarAdId { get; set; }

        [Required]
        public CarAd CarAd { get; set; } = null!;

        [Required]
        public string ReviewerId { get; set; } = null!;
        public int Rating { get; set; }

        [Required]
        [MaxLength(100)]
        public string Comment { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
