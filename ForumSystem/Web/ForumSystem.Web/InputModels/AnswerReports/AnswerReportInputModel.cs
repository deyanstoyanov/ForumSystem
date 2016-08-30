namespace ForumSystem.Web.InputModels.AnswerReports
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class AnswerReportInputModel
    {
        public int AnswerId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(ValidationConstants.ReportDescriptionMaxLength, 
            MinimumLength = ValidationConstants.ReportDescriptionMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Description { get; set; }
    }
}