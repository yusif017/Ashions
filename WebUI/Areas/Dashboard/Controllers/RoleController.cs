using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class RoleController : Controller
    {
        //dependens jeksion
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
     
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (string.IsNullOrWhiteSpace(identityRole?.Name))
            {
                ModelState.AddModelError("Name", "Role name is required.");
                return View();
            }

            var existingRole = await _roleManager.FindByNameAsync(identityRole.Name);
            if (existingRole != null)
            {
                ModelState.AddModelError("Name", "Role name already exists.");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _roleManager.CreateAsync(identityRole);
            return RedirectToAction(nameof(Index));
        }
    }
}
