using CarHub.Data.Models;
using CarHub.Service.Core.Interfaces;
using CarHub.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Areas.Admin.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Category category = new Category
            {
                Name = model.Name
            };
            await categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryFormModel model = new CategoryFormModel
            {
                Id = category.Id,
                Name = category.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await categoryService.GetByIdAsync(model.Id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = model.Name;
            await categoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}