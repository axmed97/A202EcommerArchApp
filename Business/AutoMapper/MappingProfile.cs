using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.OrderDTOs;

namespace Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderCreateDTO, Order>();
        }
    }
}
