using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        Task<bool> AddProductByLanguage(ProductAddDTO productAddDTO, string userId);
        Task<bool> EditProductByLanguage(ProductEditRecordDTO productEditRecordDTO);
        List<ProductAdminListDTO> ProductAdminListDTOs(string LangCode);
        ProductEditRecordDTO GetProductEditDTO(int id);
        List<ProductFeaturedDTO> GetProductFeaturedDTOs(string langCode);
        List<UserCartDTO> GetUserCartDTOs(List<int> ids, string langCode);
    }
}
