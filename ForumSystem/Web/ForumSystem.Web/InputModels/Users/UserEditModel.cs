namespace ForumSystem.Web.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class UserEditModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.UserNameMaxLength,
            MinimumLength = ValidationConstants.UserNameMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
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

        [StringLength(ValidationConstants.WebsiteUrlMaxLength,
            MinimumLength = ValidationConstants.WebsiteUrlMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
        [RegularExpression(ValidationConstants.WebsiteUrlRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "Website")]
        public string WebsiteUrl { get; set; }

        [StringLength(ValidationConstants.OccupationMaxLength,
            MinimumLength = ValidationConstants.OccupationMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
        public string Occupation { get; set; }

        [StringLength(ValidationConstants.InterestsMaxLength,
            MinimumLength = ValidationConstants.InterestsMinLength, 
            ErrorMessage = "{0} text must be between {2} and {1} symbols long.")]
        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.AboutMeMaxLength,
            MinimumLength = ValidationConstants.AboutMeMinLength, 
            ErrorMessage = "{0} text must be between {2} and {1} symbols long.")]
        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [StringLength(ValidationConstants.CountryMaxLength,
            MinimumLength = ValidationConstants.CountryMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
        public string Country { get; set; }

        [StringLength(ValidationConstants.CityMaxLength,
            MinimumLength = ValidationConstants.CityMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
        public string City { get; set; }

        [RegularExpression(ValidationConstants.GitHubProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "GitHub Profile")]
        public string GitHubProfile { get; set; }

        [RegularExpression(ValidationConstants.StackOverflowProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "StackOverflow Profile")]
        public string StackOverflowProfile { get; set; }

        [RegularExpression(ValidationConstants.LinkedInProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "LinkedIn Profile")]
        public string LinkedInProfile { get; set; }

        [RegularExpression(ValidationConstants.FacebookProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "Facebook Profile")]
        public string FacebookProfile { get; set; }

        [RegularExpression(ValidationConstants.TwitterProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "Twitter Profile")]
        public string TwitterProfile { get; set; }

        [StringLength(ValidationConstants.SkypeProfileMaxLength, 
            MinimumLength = ValidationConstants.SkypeProfileMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} symbols long.")]
        [RegularExpression(ValidationConstants.SkypeProfileRegEx,
            ErrorMessage = "Invalid format.")]
        [Display(Name = "Skype Profile")]
        public string SkypeProfile { get; set; }
    }
}