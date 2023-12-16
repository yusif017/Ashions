using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using System.Security.Claims;
using WebUI.Helper;
using WebUI.Utilities.Photos;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly IOxsarService _oxsarService;
        private readonly IWebHostEnvironment _env;
        private readonly string uploadsPath;
        public ProductController(IProductService productService, IHttpContextAccessor contextAccessor, ICategoryService categoryService, IWebHostEnvironment env, IColorService colorService, IOxsarService oxsarService)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _categoryService = categoryService;
            _env = env;
            _colorService = colorService;
            _oxsarService = oxsarService;
        }

        public IActionResult Index()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _productService.GetDashboardProducts(userId, "az-Az");
            return View(result.Data);
        }

        public IActionResult Create()
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var categories = _categoryService.GetAllCategories(langCode);
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            var color = _colorService.GetAllColor(langCode);
            ViewBag.ProductColor = new SelectList(color.Data, "Id", "ColorName");
            var oxsar = _oxsarService.GetAll();
            ViewBag.PraductOxsar = new SelectList(oxsar.Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductAddDTO productAdd, List<IFormFile> Photo)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var categories = _categoryService.GetAllCategories(langCode);
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            var color = _colorService.GetAllColor(langCode);
            ViewBag.ProductColor = new SelectList(color.Data, "Id", "ColorName");
            var oxsar = _oxsarService.GetAll();
            ViewBag.PraductOxsar = new SelectList(oxsar.Data, "Id", "Name");

            productAdd.PhotoUrls = await FileUplouds.SeveFileAsync(Photo, _env.WebRootPath);

            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _productService.AddProductAdmin(userId, productAdd);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {

            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var categories = _categoryService.GetAllCategories(langCode);
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            var color = _colorService.GetAllColor(langCode);
            ViewBag.ProductColor = new SelectList(color.Data, "Id", "ColorName");
            var oxsar = _oxsarService.GetAll();
            ViewBag.PraductOxsar = new SelectList(oxsar.Data, "Id", "Name");
            var edit = _productService.GetProductById(id);
            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateDTO updateDTO, List<IFormFile> Photo)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var categories = _categoryService.GetAllCategories(langCode);
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            var color = _colorService.GetAllColor(langCode);
            ViewBag.ProductColor = new SelectList(color.Data, "Id", "ColorName");
            var oxsar = _oxsarService.GetAll();
            ViewBag.PraductOxsar = new SelectList(oxsar.Data, "Id", "Name");
            if (updateDTO.Lang == null)
            {
                return View(updateDTO);
            }

            else
            {
                if (Photo != null) // Check if a new photo is uploaded
                {
                   

                    updateDTO.PhotoUrls = await FileUplouds.SeveFileAsync(Photo, _env.WebRootPath);
                }
                _productService.UpdateProduct(updateDTO);
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productService.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
