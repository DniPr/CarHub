using CarHub.Services;
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
        private readonly IFavouriteService favouriteService;

        public CarAdsController(ICarAdService carAdService, IFavouriteService favouriteService)
        {
            this.carAdService = carAdService;
            this.favouriteService = favouriteService;
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
                car.IsFavourite = await favouriteService.IsFavouriteAsync(id, userId);
            }

            return View(car);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var modelForm = await carAdService.GetCreateModelAsync();
            return View(modelForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CarAdCreateVM modelForm)
        {
            if (!ModelState.IsValid)
            {
                var vm = await carAdService.GetCreateModelAsync();
                modelForm.Categories = vm.Categories;
                return View(modelForm);
            }

            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await carAdService.CreateAsync(modelForm, ownerId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await carAdService.GetEditModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (!await carAdService.IsOwnerAsync(id, userId))
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CarAdCreateVM model , int id)
        {
            var modelFromDb = await carAdService.GetEditModelAsync(id);
            if (modelFromDb == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (!await carAdService.IsOwnerAsync(id, userId))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = modelFromDb.Categories;
                return View(model);
            }
            await carAdService.UpdateAsync(model, id);
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await carAdService.GetDeleteModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (!await carAdService.IsOwnerAsync(id, userId))
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConformation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (!await carAdService.IsOwnerAsync(id, userId))
            {
                return Unauthorized();
            }

            await carAdService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyAds()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var cars = await carAdService.GetMineAsync(userId);
            return View(cars);
        }
    }
}
