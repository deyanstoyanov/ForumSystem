namespace ForumSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}