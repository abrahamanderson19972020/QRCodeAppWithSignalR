using AutoMapper;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.DTOs.FeatureDto;
using EntitiesLayer.Entities;

namespace SignalRAPI.AutoMapping
{
    public class FeatureMapping:Profile
    {
        public FeatureMapping()
        {
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        }
    }
}
