using Core.Utilities.Results.Abstract;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> AddProductByLang(ProductAddDTO productAddDTO, string userId);
        Task<IResult> UpdateProductByLang(ProductEditRecordDTO productEditRecordDTO);
        IDataResult<List<ProductAdminListDTO>> GetAllProductAdminList(string langCode);
        IDataResult<ProductEditRecordDTO> GetProductEdit(int id);
    }
}
