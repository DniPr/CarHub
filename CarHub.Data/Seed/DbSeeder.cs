using CarHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.Data.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(CarHubDbContext context)
        {
            await SeedBrandsAsync(context);
        }

        private static async Task SeedBrandsAsync(CarHubDbContext context)
        {
            if (context.Brands.Any())
            {
                return;
            }

            var brands = new List<Brand>()
            {
                new Brand { Name = "BMW" },
                new Brand { Name = "Audi" },
                new Brand { Name = "Mercedes-Benz" },
                new Brand { Name = "Volkswagen" },
                new Brand { Name = "Toyota" },
                new Brand { Name = "Honda" },
                new Brand { Name = "Ford" },
                new Brand { Name = "Opel" },
                new Brand { Name = "Peugeot" },
                new Brand { Name = "Renault" }
            };

            await context.Brands.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }
    }
}
