using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Catalog
{

    
    public class Status
    {
        
        public int Id { get; set; }
        [Key]
        
        public bool StatusId { get; set; }
        
        [StringLength(25)]
        public string StatusDesc { get; set; }
        
        public DateTime? CreateDate { get; set; }
        
        public DateTime? DeleteDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }
        
        public int? UpdateUserId { get; set; }
    }
}
