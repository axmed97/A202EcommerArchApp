using Business.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductController(IProductService productService, ICategoryService categoryService, IHttpContextAccessor contextAccessor)
        {
            _productService = productService;
            _categoryService = categoryService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var result = _productService.GetAllProductAdminList("az-AZ");
            if (result.Success)
                return View(result.Data);

            return View();
        }
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategoriesFeatured("az-AZ");
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductAddDTO productAddDTO)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var result = await _productService.AddProductByLang(productAddDTO, userId);
            if (result.Success) return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _productService.GetProductEdit(id);
            var categories = _categoryService.GetAllCategoriesFeatured("az-AZ");
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditRecordDTO productEditRecordDTO)
        {
            var result = await _productService.UpdateProductByLang(productEditRecordDTO);
            if(result.Success)
                return RedirectToAction("Index");

            return View();
        }
    }
}
