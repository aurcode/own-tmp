using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Notifications
{
    public class NotificationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Notification> Notifications { get; set; }

        public NotificationType() 
        {
            Notifications = new List<Notification>();
        }
    }
}
