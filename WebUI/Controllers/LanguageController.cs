using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Change(string culture)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            };

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                cookieOptions
            );

            Response.Cookies.Append("Ashion", culture, cookieOptions);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Index(string id, string title)
        {
            if (id != "az-Az" && id != "ru-Ru" && id != "en-Us")
            {
                return NotFound();
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append("Ashion", id, cookieOptions);

            return RedirectToAction("Index", "Home"); 
        }
    }
    }
