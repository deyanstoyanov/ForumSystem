namespace ForumSystem.Web.Areas.Moderator.InputModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PostEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "{0} must be between {1} and {2} symbols.")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}