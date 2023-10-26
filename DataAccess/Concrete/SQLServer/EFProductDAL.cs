using Core.DataAccess.EntityFramework;
using Core.Helper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
					Pictures = null
				};
				await context.Products.AddAsync(product);
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
    }
}
