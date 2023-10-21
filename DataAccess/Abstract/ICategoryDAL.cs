using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        Task<bool> AddCategory(CategoryAddDTO categoryAddDTO, IFormFile formFile, string webRootPath);
        Task<bool> UpdateCategory(CategoryEditDTO categoryEditDTO, IFormFile formFile, string webRootPath);
        List<CategoryAdminListDTO> GetAllCategoriesAdminList(string langCode);
        List<CategoryFeaturedDTO> GetCategoryFeatureds(string langCode);
        List<CategoryNavbarDTO> GetCategoryNavbars(string langCode);
    }
}
