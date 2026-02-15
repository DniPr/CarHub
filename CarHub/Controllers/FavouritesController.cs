using CarHub.Service.Core.Interfaces;
using CarHub.Service.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarHub.Controllers
{
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly IFavouriteService favouriteService;
        public FavouritesController(IFavouriteService favouriteService)
        {
            this.favouriteService = favouriteService;
        }

        [HttpGet]
        public async Task<IActionResult> MyFavourites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await favouriteService.GetMyFavouritesAsync(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int carAdId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await favouriteService.AddAsync(carAdId, userId);

            return RedirectToAction("Details", "CarAds", new { id = carAdId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int carAdId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await favouriteService.RemoveAsync(carAdId, userId);

            return RedirectToAction("Details", "CarAds", new { id = carAdId });
        }
    }
}
