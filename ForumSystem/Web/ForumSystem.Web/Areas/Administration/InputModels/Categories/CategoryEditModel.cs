namespace ForumSystem.Web.Areas.Administration.InputModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;

    public class CategoryEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CategoryTitleMaxLength, 
            MinimumLength = ValidationConstants.CategoryTitleMinLength, 
            ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [MaxLength(ValidationConstants.CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        public int SectionId { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }
    }
}