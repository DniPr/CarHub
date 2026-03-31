using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.Category
{
    public class CategoryFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
