namespace ForumSystem.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class RegisterViewModel
    {
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

        [Required]
        [StringLength(ValidationConstants.PasswordMaxLength, 
            ErrorMessage = "The {0} must be at least {2} characters long.", 
            MinimumLength = ValidationConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}