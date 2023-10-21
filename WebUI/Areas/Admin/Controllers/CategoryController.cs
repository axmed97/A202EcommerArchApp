using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }

        public IActionResult Index()
        {
            var result = _categoryService.GetAllCategoriesAdmin("ru-RU");
            if (result.Success)
            {
                return View(result.Data);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddDTO categoryAddDTO, IFormFile Photo)
        {
            var result = await _categoryService.AddCategoryByLanguageAsync(categoryAddDTO, Photo, _env.WebRootPath);
            if (result.Success) { 
                return Redirect("/admin/category");
            }
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryAddDTO categoryAddDTO, IFormFile Photo)
        {
            var result = await _categoryService.AddCategoryByLanguageAsync(categoryAddDTO, Photo, _env.WebRootPath);
            if (result.Success)
            {
                return Redirect("/admin/category");
            }
            return View();
        }
    }
}
