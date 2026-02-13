using CarHub.Data;
using CarHub.Models;
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
        public async Task<CarAdCreateVM> GetCreateModelAsync()
        {
            var categories = await GetAllCategories();
            var model = new CarAdCreateVM
            {
                Categories = categories
            };
            return model;
        }
        public async Task CreateAsync(CarAdCreateVM model, string ownerId)
        {
            var carAd = new CarAd
            {
                Title = model.Title,
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Price = model.Price,
                Mileage = model.Mileage,
                FuelType = model.FuelType,
                Transmission = model.Transmission,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                OwnerId = ownerId,
                CreatedOn = DateTime.UtcNow
            };
            await dbContext.CarAds.AddAsync(carAd);
            await dbContext.SaveChangesAsync();
        }



        //
        public async Task<IEnumerable<CategoryDropdownVM>> GetAllCategories()
        {
            var types = await dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CategoryDropdownVM
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();
            return types;
        }
    }
}
