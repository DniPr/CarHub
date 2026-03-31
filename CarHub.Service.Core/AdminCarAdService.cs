using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core.Interfaces;
using CarHub.ViewModels.CarAdAdmin;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Service.Core
{
    public class AdminCarAdService : IAdminCarAdService
    {
        private readonly CarHubDbContext dbContext;
        public AdminCarAdService(CarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AdminCarAdListItemViewModel>> GetAllAsync()
        {
            return await dbContext.CarAds
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(c => c.Owner)
                .Select(c => new AdminCarAdListItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Price = c.Price,
                    Category = c.Category.Name,
                    Owner = c.Owner.UserName!
                })
                .ToListAsync();
        }
        public async Task<CarAd?> GetByIdAsync(int id)
        {
            return await dbContext.CarAds.FindAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var carAd = await dbContext.CarAds.FindAsync(id);
            if (carAd != null)
            {
                dbContext.CarAds.Remove(carAd);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}