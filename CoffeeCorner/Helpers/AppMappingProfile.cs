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
           
        }
    }
}
