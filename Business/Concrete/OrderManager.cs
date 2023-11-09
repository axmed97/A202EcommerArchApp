using AutoMapper;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OrderDTOs;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public OrderManager(IOrderDAL orderDAL, IMapper mapper, IProductService productService)
        {
            _orderDAL = orderDAL;
            _mapper = mapper;
            _productService = productService;
        }

        public IResult CreateOrder(List<OrderCreateDTO> orderCreateDTOs)
        {
            var result = BusinessRules.Check(CheckProducyQuantity(orderCreateDTOs));
            if (!result.Success)
            {
                return new ErrorResult();
            }
            var map = _mapper.Map<List<Order>>(orderCreateDTOs);
            _orderDAL.OrderAddRange(map);
            return new SuccessResult();
        }


        private IResult CheckProducyQuantity(List<OrderCreateDTO> orderCreateDTOs)
        {
            foreach (var item in orderCreateDTOs)
            {
                var result = _productService.GetProductByIdQuantity(item.ProductId);
                if (result.Data == 0)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}
