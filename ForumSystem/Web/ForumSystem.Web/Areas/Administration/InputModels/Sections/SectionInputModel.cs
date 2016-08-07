namespace ForumSystem.Web.Areas.Administration.InputModels.Sections
{
    using System.ComponentModel.DataAnnotations;

    public class SectionInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }
    }
}