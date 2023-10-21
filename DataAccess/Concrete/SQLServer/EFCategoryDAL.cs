using Core.DataAccess.EntityFramework;
using Core.Helper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Concrete.SQLServer
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public async Task<bool> AddCategory(CategoryAddDTO categoryAddDTO, IFormFile formFile, string webRootPath)
        {
            try
            {
                using var context = new AppDbContext();
                Category category = new()
                {
                    PhotoUrl = await formFile.SaveFileAsync(webRootPath),
                    IsFeatured = false
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                for (int i = 0; i < categoryAddDTO.CategoryName.Count; i++)
                {
                    CategoryLanguage categoryLanguage = new()
                    {
                        CategoryId = category.Id,
                        CategoryName = categoryAddDTO.CategoryName[i],
                        LangCode = categoryAddDTO.LangCode[i],

                    };
                    await context.CategoryLanguages.AddAsync(categoryLanguage);
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CategoryAdminListDTO> GetAllCategoriesAdminList(string langCode)
        {

            using var context = new AppDbContext();

            var result = context.Categories.Select(x => new CategoryAdminListDTO
            {
                Id = x.Id,
                CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                PhotoUrl = x.PhotoUrl,
                IsFeatured = x.IsFeatured
            }).ToList();

            return result;
        }

        public List<CategoryFeaturedDTO> GetCategoryFeatureds(string langCode)
        {
            using var context = new AppDbContext();

            var result = context.CategoryLanguages
                .Include(x => x.Category)
                .Where(x => x.LangCode == langCode && x.Category.IsFeatured == true)
                .Select(x => new CategoryFeaturedDTO(x.Category.Id, x.CategoryName, x.Category.PhotoUrl, x.Category.Products.Count)).ToList();

            return result;
        }

        public List<CategoryNavbarDTO> GetCategoryNavbars(string langCode)
        {
            using var context = new AppDbContext();

            var result = context.CategoryLanguages.
                Where(x => x.LangCode == langCode)
                .Include(x => x.Category)
                .Select(x => new CategoryNavbarDTO(x.Category.Id, x.CategoryName))
                .ToList();
                
            return result;
        }

        public async Task<bool> UpdateCategory(CategoryEditDTO categoryEditDTO, IFormFile formFile, string webRootPath)
        {
            try
            {
                using var context = new AppDbContext();
                var category = context.Categories.FirstOrDefault(x => x.Id == categoryEditDTO.Id);

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                var categoryLanguage = context.CategoryLanguages.FirstOrDefault(x => x.CategoryId == category.Id);

                context.CategoryLanguages.Update(categoryLanguage);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
