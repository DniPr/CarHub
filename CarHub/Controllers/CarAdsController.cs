using CarHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarHub.Controllers
{
    public class CarAdsController : Controller
    {
        private readonly ICarAdService carAdService;

        public CarAdsController(ICarAdService carAdService)
        {
            this.carAdService = carAdService;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await carAdService.GetAllAsync();
            return View(cars);
        }
        public async Task<IActionResult> Details(int id)
        {
            var car = await carAdService.GetDetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                car.IsOwner = await carAdService.IsOwnerAsync(id, userId);
            }

            return View(car);
        }
    }
}
