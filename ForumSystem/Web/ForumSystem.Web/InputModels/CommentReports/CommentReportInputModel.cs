namespace ForumSystem.Web.InputModels.CommentReports
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class CommentReportInputModel
    {
        public int CommentId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(ValidationConstants.ReportDescriptionMaxLength, 
            MinimumLength = ValidationConstants.ReportDescriptionMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Description { get; set; }
    }
}