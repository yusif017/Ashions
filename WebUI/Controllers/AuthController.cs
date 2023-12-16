using Business.Abstract;
using Entities.Concrete;

using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{


    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        /// <summary>
        /// login seyfesine getme
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        /// <summary>
        /// login olma 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var checkEmail = await _userManager.FindByEmailAsync(login.Email);
            if (checkEmail == null)
            {
                ViewBag.Error = "This email is not exist!";
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(checkEmail, login.Password, isPersistent: login.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Email or Password is valid!");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// register seyfesine getme
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        /// <summary>
        /// register olma 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {

            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var checkemail = await _userManager.FindByEmailAsync(register.Email);
            if (checkemail != null)
            {
                return View();
            }

            User newUser = new()
            {
                Firstname = register.Firstname,
                Lastname = register.Lastname,
                Email = register.Email,
                UserName = register.Email,
                PhotoUrl="/",
                Address="/"
            };

            var result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
        }
        /// <summary>
        /// logaut olma
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}

