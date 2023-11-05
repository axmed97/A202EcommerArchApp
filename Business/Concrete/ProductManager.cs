using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
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

        public IDataResult<ProductEditRecordDTO> GetProductEdit(int id)
        {
            var result = _productDAL.GetProductEditDTO(id);
            return new SuccessDataResult<ProductEditRecordDTO>(result);
        }

        public async Task<IResult> UpdateProductByLang(ProductEditRecordDTO productEditRecordDTO)
        {
            var result = await _productDAL.EditProductByLanguage(productEditRecordDTO);
            return new SuccessResult();
        }
    }
}
