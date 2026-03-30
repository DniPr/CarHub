using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Service.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly CarHubDbContext dbContext;
        public CategoryService(CarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await dbContext.Categories.FindAsync(id);
        }
        public async Task CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}