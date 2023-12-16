using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helper;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly string uploadsPath;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAdminCategories("az-AZ");
            if (result.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddDTO categoyAddDTO, IFormFile Photo)
        {
            using AppDbContext dbContext = new();

            // Check if at least one CategoryName is provided
            if (categoyAddDTO.CategoryName.All(string.IsNullOrEmpty))
            {
                // Handle the case where no CategoryName is provided
                return View();
            }

            // Check if the provided CategoryName already exists
            foreach (var categoryName in categoyAddDTO.CategoryName)
            {
                if (dbContext.Categories.Any(c => c.CategoryLanguages.Any(cl => cl.CategoryName == categoryName)))
                {
                    // Handle the case where the CategoryName already exists
                    return View();
                }
            }
            categoyAddDTO.PhotoUrl = await FileUploud.SeveFileAsync(Photo, _env.WebRootPath);
            _categoryService.AddCategory(categoyAddDTO);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var editCategory = _categoryService.GetCategoryById(id);
            return View(editCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateDTO category, IFormFile Photo)
        {
            if (Photo != null)
            {
                category.PhotoUrl = await FileUploud.SeveFileAsync(Photo, _env.WebRootPath);
            }

            _categoryService.UpdateCategory(category);
            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _categoryService.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }

    }
}