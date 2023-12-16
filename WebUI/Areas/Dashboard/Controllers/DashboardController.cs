using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class DashboardController : Controller
    {
        private readonly IContactService _contactService;

        public DashboardController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Deteil(int id)
        {

            var result = _contactService.GetContactById(id);
            if (result.Success)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(Index), "Home");

        }
        public IActionResult Index()
        {
            var result = _contactService.GetAllContent();
            if (result.Success)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
