using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task<IResult> AddCategoryByLanguageAsync(CategoryAddDTO categoryAddDTO, IFormFile formFile, string webRootPath)
        {

            var result = await _categoryDAL.AddCategory(categoryAddDTO, formFile, webRootPath);
            if (result)
            {
                return new SuccessResult("Category created successfully");
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<List<CategoryAdminListDTO>> GetAllCategoriesAdmin(string langCode)
        {
            try
            {
                var result = _categoryDAL.GetAllCategoriesAdminList(langCode);
                return new SuccessDataResult<List<CategoryAdminListDTO>>(result, "Listed");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryAdminListDTO>>(ex.Message);
            }
        }

        public IDataResult<List<CategoryFeaturedDTO>> GetAllCategoriesFeatured(string langCode)
        {
            try
            {
                var result = _categoryDAL.GetCategoryFeatureds(langCode);
                return new SuccessDataResult<List<CategoryFeaturedDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryFeaturedDTO>>(ex.Message);
            }
        }

        public IDataResult<List<CategoryNavbarDTO>> GetAllCategoriesNavbar(string langCode)
        {
            try
            {
                var result = _categoryDAL.GetCategoryNavbars(langCode);
                return new SuccessDataResult<List<CategoryNavbarDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryNavbarDTO>>(ex.Message);

            }
        }

        public IDataResult<CategoryAdminDetailDTO> GetCategoryById(int id)
        {
            var result = _categoryDAL.GetCategoryByIdAdmin(id);
            return new SuccessDataResult<CategoryAdminDetailDTO>(result);
        }

        public IResult RemoveCategory(int id)
        {
            var category = _categoryDAL.Get(x => x.Id == id);
            _categoryDAL.Delete(category);
            return new SuccessResult("Deleted Successfully");
        }

        public async Task<IResult> UpdateCategoryByLanguageAsync(CategoryAdminDetailDTO categoryEditDTO, IFormFile formFile, string webRootPath)
        {

            var result = await _categoryDAL.UpdateCategory(categoryEditDTO, formFile, webRootPath);
            if (result)
            {
                return new SuccessResult("Success");
            }
            return new ErrorResult("Error");

        }
    }
}
