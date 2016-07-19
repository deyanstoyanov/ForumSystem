namespace ForumSystem.Web.InputModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class PostEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [StringLength(100000, MinimumLength = 12, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Content { get; set; }
    }
}