using AutoMapper;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.DTOs.SocialMediaDto;
using EntitiesLayer.Entities;

namespace SignalRAPI.AutoMapping
{
    public class SocialMediaMapping:Profile
    {
        public SocialMediaMapping()
        {
            CreateMap<SocialMedia, ResultSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, GetSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, UpdateSocialMediaDto>().ReverseMap();
        }
    }
}
