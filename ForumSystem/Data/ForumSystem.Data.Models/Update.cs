namespace ForumSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    public class Update : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.UpdateReasonMinLength)]
        [MaxLength(ValidationConstants.UpdateReasonMaxLength)]
        public string Reason { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}