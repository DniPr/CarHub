using CarHub.Service.Core.Interfaces;
using CarHub.ViewModels.CarAdAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Areas.Admin.Controllers
{
    public class CarAdsController : BaseAdminController
    {
        private const int DefaultPageSize = 5;

        private readonly IAdminCarAdService carAdService;
        public CarAdsController(IAdminCarAdService carAdService)
        {
            this.carAdService = carAdService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm, int currentPage = 1)
        {
            if (currentPage < 1)
            {
                currentPage = 1;
            }

            var serviceResult = await carAdService.GetAllAsync(searchTerm, currentPage, DefaultPageSize);

            int totalPages = (int)Math.Ceiling((double)serviceResult.TotalCount / DefaultPageSize);
            if (totalPages == 0)
            {
                totalPages = 1;
            }

            AdminCarAdIndexViewModel model = new AdminCarAdIndexViewModel
            {
                CarAds = serviceResult.CarAds,
                SearchTerm = searchTerm,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                TotalCarAds = serviceResult.TotalCount,
                PageSize = DefaultPageSize
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await carAdService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}