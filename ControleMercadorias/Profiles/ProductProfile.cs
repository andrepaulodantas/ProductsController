using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using AutoMapper;
using Product.Data.Dtos;
using ControleMercadorias.Models; // Import the Product class

namespace ProductS.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, ProductItem>();
            CreateMap<UpdateProductDto, ProductItem>();
            CreateMap<ProductItem, UpdateProductDto>();
            CreateMap<ProductItem, ReadProductDto>();
        }
    }
}