using Core.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Autentication
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserTypeName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }
    }
}
