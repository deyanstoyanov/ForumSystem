namespace ForumSystem.Web.Areas.Moderator.InputModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class AnswerEditModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        [StringLength(ValidationConstants.AnswerContentMaxLength, 
            MinimumLength = ValidationConstants.AnswerContentMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.UpdateReasonMaxLength, 
            MinimumLength = ValidationConstants.UpdateReasonMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Reason { get; set; }
    }
}