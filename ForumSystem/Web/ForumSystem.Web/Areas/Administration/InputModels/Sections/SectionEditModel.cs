namespace ForumSystem.Web.Areas.Administration.InputModels.Sections
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class SectionEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.SectionTitleMaxLength, 
            MinimumLength = ValidationConstants.SectionTitleMinLength, 
            ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }
    }
}