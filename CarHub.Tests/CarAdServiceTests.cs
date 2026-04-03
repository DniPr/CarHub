using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Tests
{
    public class CarAdServiceTests
    {
        private CarHubDbContext dbContext;
        private CarAdService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CarHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new CarHubDbContext(options);
            service = new CarAdService(dbContext);

            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "Test"
            });

            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private CarAdCreateVM CreateValidModel()
        {
            return new CarAdCreateVM
            {
                Title = "Test Car",
                Brand = "BMW",
                Model = "X5",
                Year = 2020,
                Price = 10000m,
                Mileage = 150000,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Description = "Test description for car ad",
                ImageUrl = "https://test.com/car.jpg",
                CategoryId = 1
            };
        }

        [Test]
        public async Task CreateAsync_ShouldAddCarAd()
        {
            var model = new CarAdCreateVM
            {
                Title = "BMW",
                Description = "Nice car",
                Price = 10000,
                CategoryId = 1,

                Brand = "BMW",
                Model = "X5",
                FuelType = "Diesel",
                Transmission = "Automatic",
                ImageUrl = "test.jpg"
            };

            await service.CreateAsync(model, "user-id");

            Assert.AreEqual(1, dbContext.CarAds.Count());
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateCarAdSuccessfully()
        {
            var car = new CarAd
            {
                Id = 1,
                Title = "Old Title",
                Brand = "BMW",
                Model = "X3",
                Year = 2018,
                Price = 8000m,
                Mileage = 180000,
                FuelType = "Diesel",
                Transmission = "Manual",
                Description = "Old description",
                ImageUrl = "https://test.com/old.jpg",
                CategoryId = 1,
                OwnerId = "user-id",
                CreatedOn = DateTime.UtcNow
            };

            dbContext.CarAds.Add(car);
            await dbContext.SaveChangesAsync();

            var model = CreateValidModel();
            model.Title = "New Title";
            model.Brand = "Audi";
            model.Model = "A6";
            model.Price = 12000m;

            await service.UpdateAsync(model, 1);

            var updatedCar = await dbContext.CarAds.FindAsync(1);

            Assert.That(updatedCar, Is.Not.Null);
            Assert.That(updatedCar!.Title, Is.EqualTo("New Title"));
            Assert.That(updatedCar.Brand, Is.EqualTo("Audi"));
            Assert.That(updatedCar.Model, Is.EqualTo("A6"));
            Assert.That(updatedCar.Price, Is.EqualTo(12000m));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveCarAd()
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

            await service.DeleteAsync(1);

            Assert.That(dbContext.CarAds.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveCarAdAndItsFavourites()
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

            var favourite = new Favorite
            {
                CarAdId = 1,
                UserId = "other-user"
            };

            dbContext.CarAds.Add(car);
            dbContext.FavoriteCarAds.Add(favourite);
            await dbContext.SaveChangesAsync();

            await service.DeleteAsync(1);

            Assert.That(dbContext.CarAds.Count(), Is.EqualTo(0));
            Assert.That(dbContext.FavoriteCarAds.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task IsOwnerAsync_ShouldReturnTrue_WhenUserOwnsCarAd()
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

            var result = await service.IsOwnerAsync(1, "user-id");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetDetailsAsync_ShouldReturnCorrectViewModel()
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

            var result = await service.GetDetailsAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("Title"));
            Assert.That(result.Brand, Is.EqualTo("BMW"));
            Assert.That(result.CategoryName, Is.EqualTo("Test"));
        }
    }
}