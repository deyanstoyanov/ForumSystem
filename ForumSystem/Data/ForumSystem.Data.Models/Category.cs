namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    public class Category : AuditInfo, IDeletableEntity
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.CategoryTitleMinLength)]
        [MaxLength(ValidationConstants.CategoryTitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(ValidationConstants.CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        public int SectionId { get; set; }

        public virtual Section Section { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}