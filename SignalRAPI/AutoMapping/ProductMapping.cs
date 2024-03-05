using AutoMapper;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.DTOs.ProductDto;
using EntitiesLayer.Entities;

namespace SignalRAPI.AutoMapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product,ResultProductWithCategoryDto>().ReverseMap();
        }
    }
}
