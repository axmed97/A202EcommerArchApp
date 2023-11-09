using Core.Utilities.Results.Abstract;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> AddProductByLang(ProductAddDTO productAddDTO, string userId);
        Task<IResult> UpdateProductByLang(ProductEditRecordDTO productEditRecordDTO);
        IDataResult<List<ProductAdminListDTO>> GetAllProductAdminList(string langCode);
        IDataResult<ProductEditRecordDTO> GetProductEdit(int id);
        IDataResult<List<ProductFeaturedDTO>> GetProductFeaturedList(string langCode);
        IDataResult<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantities);
        IDataResult<int> GetProductByIdQuantity(int productId);
    }
}
