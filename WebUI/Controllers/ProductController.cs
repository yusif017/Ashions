using Business.Abstract;
using Core.Utilities.Concrete;
using Entities.DTOs.CartDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService  _colorService;
        public ProductController(IProductService productService, ICategoryService categoryService, IColorService colorService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
        }


        public IActionResult Detail(int id)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var praductDetel = _productService.GetProductById(id, langCode).Data;
            var praductDetelCategry = _productService.GetProductDetilCategoryPraduct(id, langCode).Data;
            var praductoxsar = _productService.GetProductDetilOxsarPraduct(id).Data;

            DetailVM vm = new()
           {
             productDetailCategoryDTOs= praductDetelCategry,
             productDetailDTOs= praductDetel,
             productDetailOxsarDTOs=praductoxsar

           };
                return View(vm);
        }
        public IActionResult Index(List<int> categoryIds, List<int> colorIds, int page = 1)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            ViewBag.CurrentPage = page;
            ViewBag.ProductCount = _productService.GetProductCount(3, categoryIds, colorIds).Data;



            var result = _productService.GetAllFilteredProducts(categoryIds, colorIds, langCode, 0, maxPrice: 10000, pageNo: page, take: 3);
            var categories = _categoryService.GetAllFilterCategories(langCode);
            var color = _colorService.GetAllFilterColor(langCode);


            ProductFilterVM productFilterVM = new()
            {
                ProductFiltereds = result.Data,
                CategoryFilters = categories.Data,
                ColorFilters = color.Data,

            };
            return View(productFilterVM);
        }

        public JsonResult GetDatas(int page, int take, string categoryList, string colorList, int minPrice, int maxPrice)
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var categories = _categoryService.GetAllFilterCategories(langCode);
            var color = _colorService.GetAllFeaturedColor(langCode);

            var cats = new List<int>();

            if (categoryList == "null")
            {
                cats = categories.Data.Select(x => x.Id).ToList();
            }
            else
            {
                cats = categoryList.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            }
            var colorcats = new List<int>();

            if (colorList == "null")
            {
                colorcats = color.Data.Select(x => x.Id).ToList();
            }
            else
            {
                colorcats = colorList.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            }

            var result = _productService.GetAllFilteredProducts(cats,colorcats, langCode, minPrice, maxPrice, pageNo: page, take: take);
            var productCount = _productService.GetProductCount(take, cats, colorcats).Data;
     

            PaginationVM paginationVM = new()
            {
                ProductCount = productCount,
                Products = result.Data,
               
                
                
            };
            return Json(paginationVM);
        }
       
    }
}
