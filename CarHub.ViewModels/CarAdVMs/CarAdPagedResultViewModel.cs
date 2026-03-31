using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.ViewModels.CarAdVMs
{
    public class CarAdPagedResultViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<CarAdListItemViewModel> CarAds { get; set; } = new List<CarAdListItemViewModel>();
    }
}
