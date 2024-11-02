using AutoMapper;
using Core.Entities.Notifications;
using Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Mappers
{
    public class MapperNotifications : Profile
    {
        public MapperNotifications() 
        {
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();

            CreateMap<NotificationType, NotificationTypeDto>();
            CreateMap<NotificationTypeDto, NotificationType>();


        }
    }
}
