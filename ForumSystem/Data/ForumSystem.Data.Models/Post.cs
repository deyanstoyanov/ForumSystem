namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        public Post()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}