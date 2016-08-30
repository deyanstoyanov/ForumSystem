namespace ForumSystem.Web.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class CommentInputModel
    {
        public int AnswerId { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        [StringLength(ValidationConstants.CommentContentMaxLength, 
            MinimumLength = ValidationConstants.CommentContentMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }
    }
}