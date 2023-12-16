using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-Az";
            }
            var IsFearch = _productService.GetIsFeacredProducts(langCode).Data;
            var Category = _categoryService.GetAllCategories(langCode).Data;
            var categories = _categoryService.GetAllFeaturedCategory(langCode).Data;
            var homme = _productService.GetHomeProducts(langCode).Data;
            HomeVM homeVM = new()
            {
             
                CategoryFeaturedDTOs = categories,
                CategoryHomeListDTO=Category, 
                ProductHomeListDTO=homme,
                PraductIsFicertDTO=IsFearch
            };
            return View(homeVM);
         
     
        }

        public IActionResult Search()
        {
         
            return View();
        }
        public JsonResult GetSearch(string title)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-Az";
            }

            var search = _productService.GetSearchProducts(langCode, title).Data;
            return Json(search);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}