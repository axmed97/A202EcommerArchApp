using Core.DataAccess.EntityFramework;
using Core.Helper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Concrete.SQLServer
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public async Task<bool> AddProductByLanguage(ProductAddDTO productAddDTO, string userId)
        {
            try
            {
                using var context = new AppDbContext();

                Product product = new()
                {
                    CategoryId = productAddDTO.CategoryId,
                    Quantity = productAddDTO.Quantity,
                    Price = productAddDTO.Price,
                    DisCount = productAddDTO.DisCount,
                    IsFeatured = productAddDTO.IsFeatured,
                    UserId = userId
                };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                List<Picture> pictures = new();

                for (int i = 0; i < productAddDTO.PhotoUrls.Count; i++)
                {
                    pictures.Add(new Picture { PhotoUrl = productAddDTO.PhotoUrls[i], ProductId = product.Id });
                }

                await context.Pictures.AddRangeAsync(pictures);
                await context.SaveChangesAsync();

                for (int i = 0; i < productAddDTO.ProductNames.Count; i++)
                {
                    ProductLanguage productLanguage = new()
                    {
                        ProductId = product.Id,
                        ProductName = productAddDTO.ProductNames[i],
                        Description = productAddDTO.Descriptions[i],
                        LangCode = i == 0 ? "az-AZ" : i == 1 ? "ru-RU" : "en-EN",
                        SeoUrl = productAddDTO.ProductNames[i].ReplaceInvalidChars()
                    };
                    await context.ProductLanguages.AddAsync(productLanguage);
                }
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductEditRecordDTO GetProductEditDTO(int id)
        {
            using var context = new AppDbContext();

            var result = context.Products
                .Where(x => x.Id == id)
                .Select(x => new ProductEditRecordDTO(
                    x.Id, 
                    x.Price, 
                    x.DisCount, 
                    x.Quantity, 
                    x.Category.Id, 
                    x.IsFeatured,
                    x.ProductLanguages.Select(y => y.ProductName).ToList(), 
                    x.ProductLanguages.Select(y => y.Description).ToList(),
                    x.ProductLanguages.Select(y => y.SeoUrl).ToList(), 
                    x.Pictures.Where(z => z.ProductId == x.Id).Select(x => x.PhotoUrl).ToList()
                    )).FirstOrDefault();

            return result;
        }

        public List<ProductAdminListDTO> ProductAdminListDTOs(string LangCode)
        {
            using var context = new AppDbContext();
            var result = context.Set<ProductAdminListDTO>().FromSqlInterpolated($"exec ProductAdminList @LangCode = {LangCode}").ToList();
            return result;
        }
    }
}
