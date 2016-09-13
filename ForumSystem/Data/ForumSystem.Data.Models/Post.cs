namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        public Post()
        {
            this.Answers = new HashSet<Answer>();
            this.Reports = new HashSet<PostReport>();
            this.Likes = new HashSet<PostLike>();
            this.PostUpdates = new HashSet<PostUpdate>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.PostTitleMinLength)]
        [MaxLength(ValidationConstants.PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [MinLength(ValidationConstants.PostContentMinLength)]
        [MaxLength(ValidationConstants.PostContentMaxLength)]
        public string Content { get; set; }

        public int Views { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public DateTime? LastActivity { get; set; }

        public bool IsLocked { get; set; }

        public string LockedById { get; set; }

        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.PostLockReasonMinLenght)]
        [MaxLength(ValidationConstants.PostLockReasonMaxLenght)]
        public string LockReason { get; set; }

        public bool IsPinned { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<PostReport> Reports { get; set; }

        public virtual ICollection<PostLike> Likes { get; set; }

        public virtual ICollection<PostUpdate> PostUpdates { get; set; }
    }
}