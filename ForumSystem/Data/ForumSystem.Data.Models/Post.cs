namespace ForumSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}