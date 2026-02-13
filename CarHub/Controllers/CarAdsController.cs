using CarHub.Services.Interfaces;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = await carAdService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CarAdCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var vm = await carAdService.GetCreateModelAsync();
                model.Categories = vm.Categories;
                return View(model);
            }

            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await carAdService.CreateAsync(model, ownerId);
            return RedirectToAction(nameof(Index));
        }
    }
}
