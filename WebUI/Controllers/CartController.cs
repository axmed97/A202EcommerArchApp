using Business.Abstract;
using Entities.DTOs.CartDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public JsonResult AddToCart(int id, int quantity)
        {
            var cartCookie = Request.Cookies["cart"];
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(15);
            cookieOptions.Secure = true;
            cookieOptions.Path = "/";

            List<CartCookieDTO> cartCookies = new();
            CartCookieDTO cartCookieDTO = new()
            {
                Id = id,
                Quantity = quantity,
            };

            if(cartCookie == null)
            {
                cartCookies.Add(cartCookieDTO); 
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(cartCookies);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            else
            {
                var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cartCookie);
                var findData = data.FirstOrDefault(x => x.Id == id);
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

        public JsonResult GetProducts()
        {
            var cartCookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cartCookie);

            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();

            var result = _productService.GetProductForCart(dataIds, langCode: "az-AZ", quantities: quantity);
            return Json(result.Data);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
