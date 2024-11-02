using Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Notifications
{
    public interface INotificationTypeService
    {
        Task<List<NotificationTypeDto>> Get();
        Task<NotificationTypeDto> Get(int id);
        Task Delete (int id);
        Task Update (NotificationTypeDto notificationType);
        Task<int> Add (NotificationTypeDto notificationType);
    }
}
