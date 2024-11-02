using Microsoft.AspNetCore.Identity;
using Core.Autentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Notifications
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Content { get; set; }
        [Required]
        public bool Seen { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime SeenDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int NotificationTypeId { get; set; }
        [Required]
        public NotificationType NotificationType { get; set; }

    }
}
