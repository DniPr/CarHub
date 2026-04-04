using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.Tests
{
    public class AdminCarAdServiceTests
    {
        private CarHubDbContext dbContext;
        private AdminCarAdService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CarHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new CarHubDbContext(options);
            service = new AdminCarAdService(dbContext);

            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "SUV"
            });

            dbContext.Users.Add(new IdentityUser
            {
                Id = "user-1",
                UserName = "pesho"
            });

            dbContext.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private CarAd CreateCarAd(int id, string title = "Test Car")
        {
            return new CarAd
            {
                Id = id,
                Title = title,
                Brand = "BMW",
                Model = "X5",
                Description = "Test description",
                FuelType = "Diesel",
                Transmission = "Automatic",
                ImageUrl = "test.jpg",
                Price = 10000,
                CategoryId = 1,
                OwnerId = "user-1"
            };
        }

        [Test]
        public async Task GetAllAsync_FiltersBySearchTerm()
        {
            dbContext.CarAds.AddRange(
                CreateCarAd(1, "BMW X5"),
                CreateCarAd(2, "Audi A4")
            );

            await dbContext.SaveChangesAsync();
            var result = await service.GetAllAsync("bmw", 1, 10);

            Assert.That(result.CarAds.Count(), Is.EqualTo(1));
            Assert.That(result.CarAds.First().Title, Does.Contain("BMW"));
        }

        [Test]
        public async Task DeleteAsync_RemovesCarAdAndFavorites()
        {
            dbContext.CarAds.Add(CreateCarAd(1));

            dbContext.FavoriteCarAds.Add(new Favorite
            {
                CarAdId = 1,
                UserId = "user-2"
            });

            await dbContext.SaveChangesAsync();
            await service.DeleteAsync(1);

            Assert.That(dbContext.CarAds.Count(), Is.EqualTo(0));
            Assert.That(dbContext.FavoriteCarAds.Count(), Is.EqualTo(0));
        }
    }
}
