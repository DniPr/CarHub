using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.Tests
{
    public class FavouriteServiceTests
    {
        private CarHubDbContext dbContext;
        private FavouriteService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CarHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new CarHubDbContext(options);
            service = new FavouriteService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddToFavourites()
        {
            var car = new CarAd
            {
                Id = 1,
                Title = "Title",
                Brand = "BMW",
                Model = "X5",
                Year = 2020,
                Price = 10000m,
                Mileage = 150000,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Description = "Description",
                ImageUrl = "https://test.com/car.jpg",
                CategoryId = 1,
                OwnerId = "user-id",
                CreatedOn = DateTime.UtcNow
            };

            dbContext.CarAds.Add(car);
            await dbContext.SaveChangesAsync();

            var favService = new FavouriteService(dbContext);

            await favService.AddAsync(1, "user2");

            Assert.That(dbContext.FavoriteCarAds.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task IsFavouriteAsync_ShouldReturnTrue_WhenExists()
        {
            dbContext.FavoriteCarAds.Add(new Favorite
            {
                CarAdId = 1,
                UserId = "user2"
            });

            await dbContext.SaveChangesAsync();

            var favService = new FavouriteService(dbContext);

            var result = await favService.IsFavouriteAsync(1, "user2");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task RemoveAsync_ShouldRemoveFromFavourites()
        {
            var fav = new Favorite
            {
                CarAdId = 1,
                UserId = "user2"
            };

            dbContext.FavoriteCarAds.Add(fav);
            await dbContext.SaveChangesAsync();

            var favService = new FavouriteService(dbContext);

            await favService.RemoveAsync(1, "user2");

            Assert.That(dbContext.FavoriteCarAds.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetMyFavouritesAsync_ShouldReturnOnlyUserFavourites()
        {
            var firstCar = new CarAd
            {
                Id = 1,
                Title = "BMW X5",
                Brand = "BMW",
                Model = "X5",
                Year = 2020,
                Price = 10000m,
                Mileage = 150000,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Description = "Description 1",
                ImageUrl = "https://test.com/car1.jpg",
                CategoryId = 1,
                OwnerId = "owner-1",
                CreatedOn = DateTime.UtcNow
            };

            var secondCar = new CarAd
            {
                Id = 2,
                Title = "Audi A6",
                Brand = "Audi",
                Model = "A6",
                Year = 2021,
                Price = 12000m,
                Mileage = 120000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Description = "Description 2",
                ImageUrl = "https://test.com/car2.jpg",
                CategoryId = 1,
                OwnerId = "owner-2",
                CreatedOn = DateTime.UtcNow.AddMinutes(-10)
            };

            dbContext.CarAds.AddRange(firstCar, secondCar);

            dbContext.FavoriteCarAds.AddRange(
                new Favorite { CarAdId = 1, UserId = "user-1" },
                new Favorite { CarAdId = 2, UserId = "user-1" },
                new Favorite { CarAdId = 1, UserId = "other-user" }
            );

            await dbContext.SaveChangesAsync();

            var result = await service.GetMyFavouritesAsync("user-1");

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(x => x.Id == 1), Is.True);
            Assert.That(result.Any(x => x.Id == 2), Is.True);
        }
    }
}
