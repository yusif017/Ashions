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

    public class OxsarController : Controller
    {
        private readonly IOxsarService _service;

        public OxsarController(IOxsarService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var result =  _service.GetAll();
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
        public async Task<IActionResult> Create(PraductOxsar praductOxsar)
        {
           
            
            _service.Add(praductOxsar);
            return RedirectToAction("Index");
        }

    }
}
