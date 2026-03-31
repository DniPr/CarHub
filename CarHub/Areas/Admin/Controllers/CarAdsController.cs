using CarHub.Service.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Areas.Admin.Controllers
{
    public class CarAdsController : BaseAdminController
    {
        private readonly IAdminCarAdService carAdService;
        public CarAdsController(IAdminCarAdService carAdService)
        {
            this.carAdService = carAdService;
        }

        public async Task<IActionResult> Index()
        {
            var ads = await carAdService.GetAllAsync();
            return View(ads);
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