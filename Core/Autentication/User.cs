using Microsoft.AspNetCore.Identity;
using Core.Entities.Catalog;
using Core.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Autentication
{
    //
    public class User : IdentityUser<int>
    {
        //public int EmployeeId { get; set; }
        public int UserType { get; set; }
        //public int AreaId { get; set; }
        public string? Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public List<Notification> Notifications { get; set; }

        public User() { 
            Notifications = new List<Notification>();
        }
    }
}
