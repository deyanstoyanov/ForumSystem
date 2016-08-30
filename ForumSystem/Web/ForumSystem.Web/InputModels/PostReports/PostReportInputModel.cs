namespace ForumSystem.Web.InputModels.PostReports
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class PostReportInputModel
    {
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(ValidationConstants.ReportDescriptionMaxLength, 
            MinimumLength = ValidationConstants.ReportDescriptionMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Description { get; set; }
    }
}