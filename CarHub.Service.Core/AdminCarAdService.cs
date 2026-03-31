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

        public async Task<AdminCarAdPagedResultViewModel> GetAllAsync(string? searchTerm, int currentPage, int pageSize)
        {
            IQueryable<CarAd> query = dbContext.CarAds
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(c => c.Owner);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string normalizedSearch = searchTerm.Trim().ToLower();

                query = query.Where(c =>
                    c.Title.ToLower().Contains(normalizedSearch) ||
                    c.Category.Name.ToLower().Contains(normalizedSearch) ||
                    c.Owner.UserName!.ToLower().Contains(normalizedSearch));
            }

            int totalCount = await query.CountAsync();

            IEnumerable<AdminCarAdListItemViewModel> carAds = await query
                .OrderByDescending(c => c.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new AdminCarAdListItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Price = c.Price,
                    Category = c.Category.Name,
                    Owner = c.Owner.UserName ?? "Unknown"
                })
                .ToListAsync();

            return new AdminCarAdPagedResultViewModel
            {
                CarAds = carAds,
                TotalCount = totalCount
            };
        }
        public async Task<CarAd?> GetByIdAsync(int id)
        {
            return await dbContext.CarAds.FindAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var carAd = await dbContext.CarAds.FindAsync(id);
            if (carAd == null)
            {
                return;
            }
            var favorites = dbContext.FavoriteCarAds
                .Where(f => f.CarAdId == id);

            dbContext.FavoriteCarAds.RemoveRange(favorites);
            dbContext.CarAds.Remove(carAd);
            await dbContext.SaveChangesAsync();
        }
    }
}