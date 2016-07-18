﻿namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        [StringLength(200, MinimumLength = 2, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}