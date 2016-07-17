namespace ForumSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        public int SectionId { get; set; }

        public virtual Section Section { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}