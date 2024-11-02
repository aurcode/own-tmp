using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Notifications;

namespace EntityFrameworkCore.Repository.Notification
{
    public interface INotificationRepository : IRepository<int, Core.Entities.Notifications.Notification>
    {
        Task<IEnumerable<Core.Entities.Notifications.Notification>> GetByUserIdAsync(int id, int number);

    }
}
