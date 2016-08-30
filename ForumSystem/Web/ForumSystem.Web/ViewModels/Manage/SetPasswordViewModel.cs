namespace ForumSystem.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(ValidationConstants.PasswordMaxLength, 
            ErrorMessage = "The {0} must be at least {2} characters long.", 
            MinimumLength = ValidationConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}