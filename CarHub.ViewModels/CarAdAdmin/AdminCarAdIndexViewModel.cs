using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdAdmin
{
    public class AdminCarAdIndexViewModel
    {
        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCarAds { get; set; }
        public int PageSize { get; set; } = 5;
        public IEnumerable<AdminCarAdListItemViewModel> CarAds { get; set; } = new List<AdminCarAdListItemViewModel>();

    }
}
