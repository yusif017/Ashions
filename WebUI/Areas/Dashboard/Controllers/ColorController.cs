using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ColorDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helper;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class ColorController : Controller
    {
        private readonly IColorService _Service;

        public ColorController(IColorService service)
        {
            _Service = service;
        }

        public async Task<IActionResult> Index()
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }
            var result = await _Service.GetAllAdminColor(langCode);
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
        public async Task<IActionResult> Create(ColorAddDTO colorAddDTO)
        {
          
            _Service.AddColor(colorAddDTO);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var edit = _Service.GetColorById(id);
            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ColorUpdateDTO color)
        {
        

            _Service.UpdateColor(color);
                return RedirectToAction(nameof(Index));
            

        }

        [HttpPost]
        public IActionResult Delete(ProductColor color)
        {
            _Service.DeleteColor(color);
            return RedirectToAction(nameof(Index));
        }

    }
}