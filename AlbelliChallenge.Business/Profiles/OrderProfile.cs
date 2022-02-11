using AlbelliChallenge.Business.Models;
using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Models;
using AutoMapper;

namespace AlbelliChallenge.Business.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDetailsPersistence, OrderDetails>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.RequiredBinWidth, opt => opt.MapFrom(src => src.RequiredBinWidth));

            CreateMap<ProductPersitence, ProductDetails>()
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<Order, OrderDetailsPersistence>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<ProductDetails, ProductPersitence>()
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}