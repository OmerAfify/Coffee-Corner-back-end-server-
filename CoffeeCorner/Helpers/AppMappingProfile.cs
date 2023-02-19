using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeCorner.DTOs;
using CoffeeCorner.Helpers.ValueResolvers;
using Domains.Models;

namespace CoffeeCorner.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
           CreateMap<Product, ProductDTO>()
                .ForMember(d=>d.BrandName,opt=>opt.MapFrom(s=>s.ProductBrand.ProductBrandName))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ProductBrand,ProductBrandDTO>().ReverseMap();

            CreateMap<Address,AddressDTO>().ReverseMap();
            
            
            CreateMap<OrderAddress,OrderAddressDTO>().ReverseMap();



            CreateMap<OrderReturnedDTO, Order>().ReverseMap()
             .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
             .ForMember(d => d.Status, opt => opt.MapFrom(s => s.OrderStatus.StatusName))
             .ForMember(d => d.DeliveryPrice, opt => opt.MapFrom(s=>s.DeliveryMethod.DeliveryPrice));

    CreateMap<OrderedItemDTO, OrderedItem>().ReverseMap()
             .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductItemOrdered.ProductId))
             .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.ProductItemOrdered.ProductName))
             .ForMember(d => d.ProductSalesPrice, opt => opt.MapFrom(s=>s.ProductItemOrdered.SalesPrice))
             .ForMember(d => d.PictureUrl, opt => opt.MapFrom<OrderPictureUrlResolver>());




        }
    }
}
