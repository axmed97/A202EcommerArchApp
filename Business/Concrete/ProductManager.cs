using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }
        public async Task<IResult> AddProductByLang(ProductAddDTO productAddDTO, string userId)
        {
            var result = await _productDAL.AddProductByLanguage(productAddDTO, userId);
            if (result)
                return new SuccessResult();

            return new ErrorResult();
        }
        public IDataResult<List<ProductAdminListDTO>> GetAllProductAdminList(string langCode)
        {
            var result = _productDAL.ProductAdminListDTOs(langCode);
            return new SuccessDataResult<List<ProductAdminListDTO>>(result);
        }

        public IDataResult<int> GetProductByIdQuantity(int productId)
        {
            var result = _productDAL.Get(x => x.Id == productId).Quantity;
            return new SuccessDataResult<int>(result);
        }

        public IDataResult<ProductEditRecordDTO> GetProductEdit(int id)
        {
            var result = _productDAL.GetProductEditDTO(id);
            return new SuccessDataResult<ProductEditRecordDTO>(result);
        }
        public IDataResult<List<ProductFeaturedDTO>> GetProductFeaturedList(string langCode)
        {
            var result = _productDAL.GetProductFeaturedDTOs(langCode);
            return new SuccessDataResult<List<ProductFeaturedDTO>>(result);
        }

        public IDataResult<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantities)
        {
            var result = _productDAL.GetUserCartDTOs(ids, langCode);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Quantity = quantities[i];
            }
            return new SuccessDataResult<List<UserCartDTO>>(result);
        }

        public async Task<IResult> UpdateProductByLang(ProductEditRecordDTO productEditRecordDTO)
        {
            var result = await _productDAL.EditProductByLanguage(productEditRecordDTO);
            return new SuccessResult();
        }
    }
}
