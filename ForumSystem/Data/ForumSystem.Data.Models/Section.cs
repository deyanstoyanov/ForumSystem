namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    public class Section : AuditInfo, IDeletableEntity
    {
        public Section()
        {
            this.Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.SectionTitleMinLength)]
        [MaxLength(ValidationConstants.SectionTitleMaxLength)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}