namespace ForumSystem.Web.Areas.Administration.InputModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CategoryInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int SectionId { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }
    }
}