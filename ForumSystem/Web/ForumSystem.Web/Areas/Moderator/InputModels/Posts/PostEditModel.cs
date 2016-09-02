namespace ForumSystem.Web.Areas.Moderator.InputModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class PostEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.PostTitleMaxLength,
            MinimumLength = ValidationConstants.PostTitleMinLength, 
            ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [StringLength(ValidationConstants.PostContentMaxLength, 
            MinimumLength = ValidationConstants.PostContentMinLength, 
            ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.UpdateReasonMaxLength, 
            MinimumLength = ValidationConstants.UpdateReasonMinLength, 
            ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Reason { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}