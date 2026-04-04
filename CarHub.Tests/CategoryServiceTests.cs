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
    public class CategoryServiceTests
    {
        private CarHubDbContext dbContext;
        private CategoryService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CarHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new CarHubDbContext(options);
            service = new CategoryService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllCategories()
        {
            dbContext.Categories.AddRange(
                new Category { Id = 1, Name = "SUV" },
                new Category { Id = 2, Name = "Sedan" }
            );

            await dbContext.SaveChangesAsync();
            var result = await service.GetAllAsync();
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectCategory()
        {
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "SUV"
            });

            await dbContext.SaveChangesAsync();

            var result = await service.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("SUV"));
        }

        [Test]
        public async Task CreateAsync_ShouldAddCategory()
        {
            var category = new Category
            {
                Id = 1,
                Name = "SUV"
            };

            await service.CreateAsync(category);
            Assert.That(dbContext.Categories.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateCategory()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Old Name"
            };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            category.Name = "New Name";
            await service.UpdateAsync(category);

            var updatedCategory = await dbContext.Categories.FindAsync(1);

            Assert.That(updatedCategory, Is.Not.Null);
            Assert.That(updatedCategory!.Name, Is.EqualTo("New Name"));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveCategory()
        {
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "SUV"
            });

            await dbContext.SaveChangesAsync();
            await service.DeleteAsync(1);

            Assert.That(dbContext.Categories.Count(), Is.EqualTo(0));
        }
    }
}
