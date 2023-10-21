using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public CategoryController(ICategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetCategories(string langCode)
        {
            var result = _categoryService.GetAllCategoriesAdmin(langCode);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(IFormFile Photo, CategoryAddDTO categoryAddDTO)
        {
            var result = await _categoryService.AddCategoryByLanguageAsync(categoryAddDTO, Photo, _env.WebRootPath);
            return Ok(result);
        }
    }
}
