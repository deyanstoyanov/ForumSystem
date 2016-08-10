namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Common.Models;

    public class Answer : AuditInfo, IDeletableEntity
    {
        public Answer()
        {
            this.Comments = new HashSet<Comment>();
            this.Reports = new HashSet<AnswerReport>();
            this.Likes = new HashSet<AnswerLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "The {0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<AnswerReport> Reports { get; set; }

        public virtual ICollection<AnswerLike> Likes { get; set; }

    }
}