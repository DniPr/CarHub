using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Seller { get; set; } = null!;
    }
}
