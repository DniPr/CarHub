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

            dbContext.Categories.Add(CreateCategory());
            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private Category CreateCategory(int id = 1, string name = "Test")
        {
            return new Category
            {
                Id = id,
                Name = name
            };
        }

        private IdentityUser CreateUser(string id = "user-1", string userName = "pesho")
        {
            return new IdentityUser
            {
                Id = id,
                UserName = userName
            };
        }

        private CarAd CreateCarAd(
            int id,
            string title = "BMW X5",
            string brand = "BMW",
            string model = "X5",
            string ownerId = "user-1",
            int categoryId = 1,
            DateTime? createdOn = null)
        {
            return new CarAd
            {
                Id = id,
                Title = title,
                Brand = brand,
                Model = model,
                Year = 2020,
                Price = 10000m,
                Mileage = 150000,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Description = "Test description",
                ImageUrl = "https://test.com/car.jpg",
                CategoryId = categoryId,
                OwnerId = ownerId,
                CreatedOn = createdOn ?? DateTime.UtcNow
            };
        }

        private Favorite CreateFavorite(int carAdId, string userId = "other-user")
        {
            return new Favorite
            {
                CarAdId = carAdId,
                UserId = userId
            };
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
            var user = CreateUser("user-101", "ivan");
            dbContext.Users.Add(user);

            dbContext.CarAds.AddRange(
                CreateCarAd(
                    id: 1,
                    title: "Car 1",
                    brand: "BMW",
                    model: "X1",
                    ownerId: user.Id,
                    createdOn: new DateTime(2026, 1, 1)),
                CreateCarAd(
                    id: 2,
                    title: "Car 2",
                    brand: "Audi",
                    model: "Q5",
                    ownerId: user.Id,
                    createdOn: new DateTime(2026, 2, 1)),
                CreateCarAd(
                    id: 3,
                    title: "Car 3",
                    brand: "Mercedes",
                    model: "GLC",
                    ownerId: user.Id,
                    createdOn: new DateTime(2026, 3, 1))
            );

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
            var car = CreateCarAd(
                id: 1,
                title: "Old Title",
                brand: "BMW",
                model: "X3",
                ownerId: "user-id");

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
            var car = CreateCarAd(1, ownerId: "user-id");
            var favourite = CreateFavorite(1);

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