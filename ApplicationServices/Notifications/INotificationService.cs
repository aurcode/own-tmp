using Core.Entities.Notifications;
using Dto;
using Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Notifications
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> Get();
        Task<NotificationDto> Get(int id);
        Task Delete(int id);
        Task Update(NotificationDto notification);
        Task AddNotificationsToRole(NotificationRolesDto notification);

        Task AddNotificationsToAll(NotificationToAllDto notification);


        Task AddNotifications(NotificationDto notification);

        Task<List<NotificationDto>> GetByUserIdAsync(int userId, IdDto id);
    }
}
