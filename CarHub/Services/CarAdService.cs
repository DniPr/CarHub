using CarHub.Data;
using CarHub.Models;
using CarHub.Services.Interfaces;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<CarAdCreateVM?> GetEditModelAsync(int id)
        {
            var model = await dbContext.CarAds
                .AsNoTracking()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new ArgumentException("Car ad not found.");
            }
            CarAdCreateVM? editModel = new CarAdCreateVM
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
                Categories = await GetAllCategories()
            };
            return editModel;
        }
        public async Task UpdateAsync(CarAdCreateVM model,int id)
        {
            var car = await dbContext.CarAds.FindAsync(id);

            if (car == null)
            {
                throw new ArgumentException("Car Ad not found");
            }

            car.Title = model.Title;
            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Year = model.Year;
            car.Price = model.Price;
            car.Mileage = model.Mileage;
            car.FuelType = model.FuelType;
            car.Transmission = model.Transmission;
            car.Description = model.Description;
            car.ImageUrl = model.ImageUrl;
            car.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();
        }
        public async Task<CarAdDeleteVM?> GetDeleteModelAsync(int id)
        {
            return await dbContext.CarAds
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CarAdDeleteVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl
                })
                .FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var car = await dbContext.CarAds.FindAsync(id);
            if (car == null)
            {
                return;
            }

            //var favorites = dbContext.Favorites.Where(f => f.CarAdId == id);
            //dbContext.Favorites.RemoveRange(favorites);

            dbContext.CarAds.Remove(car);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CarAdIndexVM>> GetMineAsync(string userId)
        {
            return await dbContext.CarAds
                .AsNoTracking()
                .Where(c => c.OwnerId == userId)
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
                .ToListAsync();
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
