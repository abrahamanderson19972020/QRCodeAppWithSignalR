using AutoMapper;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.DTOs.DiscountDto;
using EntitiesLayer.Entities;

namespace SignalRAPI.AutoMapping
{
    public class DiscountMapping:Profile
    {
        public DiscountMapping()
        {
            CreateMap<Discount, ResultDiscountDto>().ReverseMap();
            CreateMap<Discount, CreateDiscountDto>().ReverseMap();
            CreateMap<Discount, GetDiscountDto>().ReverseMap();
            CreateMap<Discount, UpdateDiscountDto>().ReverseMap();
        }
    }
}
