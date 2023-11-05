using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult> AddCategoryByLanguageAsync(CategoryAddDTO categoryAddDTO, IFormFile formFile, string webRootPath);
        Task<IResult> UpdateCategoryByLanguageAsync(CategoryAdminDetailDTO categoryEditDTO, IFormFile formFile, string webRootPath);
        IResult RemoveCategory(int id);
        IDataResult<List<CategoryAdminListDTO>> GetAllCategoriesAdmin(string langCode);
        IDataResult<List<CategoryFeaturedDTO>> GetAllCategoriesFeatured(string langCode);
        IDataResult<List<CategoryNavbarDTO>> GetAllCategoriesNavbar(string langCode);
        IDataResult<CategoryAdminDetailDTO> GetCategoryById(int id);
    }
}
