using Business.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Concrete.ErrorResult;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, IHttpContextAccessor contextAccessor, IUserService userService, IOrderService orderService)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _orderService = orderService;
        }


        public JsonResult AddToCart(string id, int quantity)
        {
            var cookie = Request.Cookies["cart"];

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";

            List<CartCookieDTO> cartCookies = new();

            CartCookieDTO cartCookieDTO = new()
            {
                Id = Convert.ToInt32(id),
                Quantity = quantity
            };
            if (cookie == null)
            {
                cartCookies.Add(cartCookieDTO);
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(cartCookies);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            else
            {
                var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
                var findData = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

                if (findData != null)
                {
                    findData.Quantity += 1;
                }
                else
                {
                    data.Add(cartCookieDTO);
                }
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            return Json("");
        }


        public IActionResult UserCart()
        {
            return View();
        }

        public JsonResult DecreaseQuantity(string id)
        {
            var cookie = Request.Cookies["cart"];
            var cookieOptions = new CookieOptions();

            List<CartCookieDTO> data;

            if (cookie == null)
            {
                return Json("error"); // Çerez yoksa hata döndür
            }
            else
            {
                data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            }

            var result = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

            if (result != null)
            {
                if (result.Quantity > 1) // Miktar 1'den büyükse azalt
                {
                    result.Quantity -= 1;
                }
                else
                {
                    return Json("minQuantity"); // Miktar 1'e ulaştıysa hata döndür
                }
            }
            else
            {
                return Json("error"); // Ürün bulunamadıysa hata döndür
            }

            var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
            Response.Cookies.Append("cart", cookieJson, cookieOptions);

            return Json("ok");
        }

        public JsonResult GetProduct()
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }


            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, langCode, quantity);
            return Json(result);
        }
        public JsonResult AddToCartQuery(string id)
{
    var cookie = Request.Cookies["cart"];
    var cookieOptions = new CookieOptions();

    List<CartCookieDTO> data;

    if (cookie == null)
    {
        return Json("error"); // Çerez yoksa hata döndür
    }
    else
    {
        data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
    }

    var result = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

    if (result != null)
    {
        if (result.Quantity < 10) // Miktar 10'dan azsa artır
        {
            result.Quantity += 1;
        }
        else
        {
            return Json("maxQuantity"); // Miktar 10'a ulaştıysa hata döndür
        }
    }
    else
    {
        return Json("error"); // Ürün bulunamadıysa hata döndür
    }
            var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
            Response.Cookies.Append("cart", cookieJson, cookieOptions);

            return Json("ok");
        }

        public JsonResult RemoveData(string id)
        {
            var cookie = Request.Cookies["cart"];
            var cookieOptions = new CookieOptions();


            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var result = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

            data.Remove(result);

            var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
            Response.Cookies.Append("cart", cookieJson, cookieOptions);

            return Json("ok");
        }

        public IActionResult Checkout()
        {
            string langCode = Request.Cookies["Ashion"];

            if (string.IsNullOrEmpty(langCode))
            {
                langCode = "az-AZ";
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserById(userId);

            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, langCode, quantity);

            CheckoutVM vm = new()
            {
                CartItems = result.Data,
                User = user.Data
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Checkout(string id)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserById(userId);

            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, "az-Az", quantity);

            List<OrderCreateDTO> orderList = new();

            foreach (var item in result.Data)
            {
                OrderCreateDTO orderCreateDTO = new OrderCreateDTO
                {
                    UserId = userId,
                    ProductId = item.Id,
                    ProductPrice = item.Price,
                    Quantity = item.Quantity,
                    Message = "Yoxdu"
                };

                orderList.Add(orderCreateDTO);
            }

            // Ürün miktarını kontrol et
            var quantityCheckResult = _orderService.CheckProductQuantity(orderList);
            if (quantityCheckResult is ErrorResult)
            {
                TempData["ErrorMessage"] = "Ürünlerin bazıları stokta yok.";
                return RedirectToAction("Checkout", "Cart");
            }

            // Ürün miktar sınırlarını kontrol et
            var quantityLimitCheckResult = _orderService.CheckProductQuantityLimit(orderList);
            if (quantityLimitCheckResult is ErrorResult)
            {
                TempData["ErrorMessage"] = "Ürünlerin bazıları için miktar sınırını aştınız.";

                return RedirectToAction("Checkout", "Cart");
            }

      

            _orderService.CreateOrder(userId, orderList);
            Response.Cookies.Delete("cart");

            return RedirectToAction("Index", "Home");
        }

    }
}
