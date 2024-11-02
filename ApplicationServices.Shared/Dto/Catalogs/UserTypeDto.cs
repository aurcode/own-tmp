using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Catalog
{
    public class UserTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool? StatusId { get; set; }
    }
}
