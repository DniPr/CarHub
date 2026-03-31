using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdAdmin
{
    public class AdminCarAdPagedResultViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<AdminCarAdListItemViewModel> CarAds { get; set; } = new List<AdminCarAdListItemViewModel>();
    }
}
