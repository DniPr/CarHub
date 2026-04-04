using CarHub.Data;
using CarHub.Data.Models;
using CarHub.Service.Core;
using CarHub.ViewModels.CarAdVMs;
using Microsoft.AspNetCore.Identity;
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
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoCars()
        {
            var result = await service.GetAllAsync(null, 1, 10);

            Assert.That(result.CarAds.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnPagedCarsOrderedByIdDescending()
        {
            dbContext.Users.Add(new IdentityUser
            {
                Id = "user-101",
                UserName = "ivan"
            });

            dbContext.CarAds.AddRange(
                new CarAd
                {
                    Id = 1,
                    Title = "Car 1",
                    Brand = "BMW",
                    Model = "X1",
                    Year = 2018,
                    Price = 8000m,
                    Mileage = 180000,
                    FuelType = "Diesel",
                    Transmission = "Manual",
                    Description = "Car 1",
                    ImageUrl = "https://test.com/1.jpg",
                    CategoryId = 1,
                    OwnerId = "user-101",
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new CarAd
                {
                    Id = 2,
                    Title = "Car 2",
                    Brand = "Audi",
                    Model = "Q5",
                    Year = 2019,
                    Price = 12000m,
                    Mileage = 140000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Description = "Car 2",
                    ImageUrl = "https://test.com/2.jpg",
                    CategoryId = 1,
                    OwnerId = "user-101",
                    CreatedOn = new DateTime(2026, 2, 1)
                },
                new CarAd
                {
                    Id = 3,
                    Title = "Car 3",
                    Brand = "Mercedes",
                    Model = "GLC",
                    Year = 2020,
                    Price = 18000m,
                    Mileage = 100000,
                    FuelType = "Diesel",
                    Transmission = "Automatic",
                    Description = "Car 3",
                    ImageUrl = "https://test.com/3.jpg",
                    CategoryId = 1,
                    OwnerId = "user-101",
                    CreatedOn = new DateTime(2026, 3, 1)
                });

            await dbContext.SaveChangesAsync();

            var result = await service.GetAllAsync(null, 2, 1);

            Assert.That(result.TotalCount, Is.EqualTo(3));
            Assert.That(result.CarAds.Count(), Is.EqualTo(1));
            Assert.That(result.CarAds.First().Title, Is.EqualTo("Car 2"));
        }

        [Test]
        public async Task CreateAsync_ShouldSaveCarCorrectly()
        {
            var model = CreateValidModel();

            await service.CreateAsync(model, "user-id");

            var car = dbContext.CarAds.First();

            Assert.That(car.Title, Is.EqualTo(model.Title));
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
        public void DeleteAsync_ShouldNotThrow_WhenNotFound()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await service.DeleteAsync(999);
            });

            Assert.That(dbContext.CarAds.Count(), Is.EqualTo(0));
        }
    }
}