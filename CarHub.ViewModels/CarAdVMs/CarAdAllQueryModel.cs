using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdAllQueryModel
    {
        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCarAds { get; set; }
        public int PageSize { get; set; } = 6;
        public IEnumerable<CarAdListItemViewModel> CarAds { get; set; } = new List<CarAdListItemViewModel>();

    }
}
