using AutoMapper;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.DTOs.NotificationDto;
using EntitiesLayer.Entities;

namespace SignalRAPI.AutoMapping
{
    public class NotificationMapping:Profile
    {
        public NotificationMapping()
        {
            CreateMap<Notification, ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, GetNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
        }
    }
}
