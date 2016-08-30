namespace ForumSystem.Web.InputModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class PostEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(ValidationConstants.PostTitleMaxLength,
            MinimumLength = ValidationConstants.PostTitleMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        [StringLength(ValidationConstants.PostContentMaxLength,
            MinimumLength = ValidationConstants.PostContentMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }
    }
}