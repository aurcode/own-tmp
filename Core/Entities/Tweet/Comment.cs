using Core.Autentication;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Tweet
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int TweetId { get; set; }
        [Required]
        public Tweet Tweet { get; set; }
    }
}
