namespace ForumSystem.Web.InputModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PostEditModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Title")]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [Display(Name = "Content")]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }
    }
}