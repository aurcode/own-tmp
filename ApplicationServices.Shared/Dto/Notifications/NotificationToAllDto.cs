using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Notifications
{
    public class NotificationToAllDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Seen { get; set; }
        public DateTime Created { get; set; }
        public DateTime? SeenDate { get; set; }

        public NotificationTypeDto NotificationType { get; set; }

        public int NotificationTypeId { get; set; }
    }
}
