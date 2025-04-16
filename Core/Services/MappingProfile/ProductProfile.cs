using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfile
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(D => D.BrandName, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.TypeName, O => O.MapFrom(S => S.ProductType.Name));
            CreateMap<ProductBrand,BrandResultDto>();
            CreateMap<ProductType,TypeResultDto>();
        }
    }
}
