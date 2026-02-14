using CarHub.Data;
using CarHub.Models;
using CarHub.Services.Interfaces;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly CarHubDbContext dbContext;
        public FavouriteService(CarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(int carAdId, string userId)
        {
            bool exists = await dbContext.FavoriteCarAds
                .AnyAsync(f => f.CarAdId == carAdId && f.UserId == userId);
            if (!exists)
            {
                throw new ArgumentException("An error has occured");
            }

            var favorite = new Favorite
            {
                CarAdId = carAdId,
                UserId = userId
            };

            dbContext.FavoriteCarAds.Add(favorite);
            await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CarAdIndexVM>> GetMyFavoritesAsync(string userId)
        {
            return await dbContext.FavoriteCarAds
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CarAd.CreatedOn)
                .Select(f => new CarAdIndexVM
                {
                    Id = f.CarAd.Id,
                    Title = f.CarAd.Title,
                    Brand = f.CarAd.Brand,
                    Model = f.CarAd.Model,
                    Year = f.CarAd.Year,
                    Price = f.CarAd.Price,
                    ImageUrl = f.CarAd.ImageUrl
                })
                .ToListAsync();
        }
        public async Task<bool> IsFavoriteAsync(int carAdId, string userId)
        {
            return await dbContext.FavoriteCarAds
                .AsNoTracking()
                .AnyAsync(f => f.CarAdId == carAdId && f.UserId == userId);
        }
        public async Task RemoveAsync(int carAdId, string userId)
        {
            var favorite = await dbContext.FavoriteCarAds
                .FirstOrDefaultAsync(f => f.CarAdId == carAdId && f.UserId == userId);

            if (favorite == null)
            {
                throw new ArgumentException("An error has occured");
            }

            dbContext.FavoriteCarAds.Remove(favorite);
            await dbContext.SaveChangesAsync();
        }
    }
}
