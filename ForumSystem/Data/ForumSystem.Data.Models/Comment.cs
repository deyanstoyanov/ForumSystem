namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    public class Comment : AuditInfo, IDeletableEntity
    {
        public Comment()
        {
            this.Reports = new HashSet<CommentReport>();
            this.Likes = new HashSet<CommentLike>();
            this.Updates = new HashSet<CommentUpdate>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.CommentContentMinLength)]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<CommentReport> Reports { get; set; }

        public virtual ICollection<CommentLike> Likes { get; set; }

        public virtual ICollection<CommentUpdate> Updates { get; set; }
    }
}