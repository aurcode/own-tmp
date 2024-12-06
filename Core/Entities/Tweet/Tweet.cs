using Core.Autentication;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Tweet
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        [Required]
        public int Views {  get; set; } = 0;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
