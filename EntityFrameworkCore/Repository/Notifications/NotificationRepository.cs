using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repository.Notification
{
    public class NotificationRepository : Repository<int, Core.Entities.Notifications.Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        

        public async Task<IEnumerable<Core.Entities.Notifications.Notification>> GetByUserIdAsync(int id,int number)
        {
            var x =  Context.Notifications.Include(x=>x.NotificationType).Where(_ => _.UserId == id).OrderByDescending(x => x.Created).Take(number);
            return x;
        }

        
    }
}
