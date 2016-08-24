namespace ForumSystem.Web.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class UserEditModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }

        [Display(Name = "Website")]
        public string WebsiteUrl { get; set; }

        public string Occupation { get; set; }

        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 6, ErrorMessage = "{0} text must be between {2} and {1} symbols long.")]
        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Display(Name = "GitHub Profile")]
        public string GitHubProfile { get; set; }

        [Display(Name = "StackOverflow Profile")]
        public string StackOverflowProfile { get; set; }

        [Display(Name = "LinkedIn Profile")]
        public string LinkedInProfile { get; set; }

        [Display(Name = "Facebook Profile")]
        public string FacebookProfile { get; set; }

        [Display(Name = "Twitter Profile")]
        public string TwitterProfile { get; set; }

        [Display(Name = "Skype Profile")]
        public string SkypeProfile { get; set; }
    }
}