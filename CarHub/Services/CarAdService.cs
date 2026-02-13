using CarHub.Data;
using CarHub.Services.Interfaces;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Services
{
    public class CarAdService : ICarAdService
    {
        private readonly CarHubDbContext dbContext;
        public CarAdService(CarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CarAdIndexVM>> GetAllAsync()
        {
            return await dbContext.CarAds
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new CarAdIndexVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl
                })
                .ToArrayAsync();
        }

        public async Task<CarAdDetailsVM?> GetDetailsAsync(int id)
        {
            return await dbContext.CarAds
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CarAdDetailsVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Price = c.Price,
                    Mileage = c.Mileage,
                    FuelType = c.FuelType,
                    Transmission = c.Transmission,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    CategoryName = c.Category.Name,
                    CreatedOn = c.CreatedOn,
                    IsOwner = false
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsOwnerAsync(int carAdId, string userId)
        {
            return await dbContext.CarAds
                .AsNoTracking()
                .AnyAsync(c => c.Id == carAdId && c.OwnerId.ToLower() == userId.ToLower());
        }
    }
}
