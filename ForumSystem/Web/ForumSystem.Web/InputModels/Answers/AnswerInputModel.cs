namespace ForumSystem.Web.InputModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class AnswerInputModel
    {
        public int PostId { get; set; }

        public string Post { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        [StringLength(ValidationConstants.AnswerContentMaxLength, 
            MinimumLength = ValidationConstants.AnswerContentMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }
    }
}