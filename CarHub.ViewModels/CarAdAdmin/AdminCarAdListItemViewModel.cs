using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdAdmin
{
    public class AdminCarAdListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string Owner { get; set; } = null!;
    }
}
