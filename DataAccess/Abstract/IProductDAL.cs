using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        Task<bool> AddProductByLanguage(ProductAddDTO productAddDTO, string userId);
        List<ProductAdminListDTO> ProductAdminListDTOs(string LangCode);
        ProductEditRecordDTO GetProductEditDTO(int id);
    }
}
