namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Common.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Posts = new HashSet<Post>();
            this.Answers = new HashSet<Answer>();
            this.Comments = new HashSet<Comment>();
            this.ReceivedNotifications = new HashSet<Notification>();
            this.SentNotifications = new HashSet<Notification>();

            this.CreatedOn = DateTime.Now;
        }

        public string PictureUrl { get; set; }

        [MinLength(ValidationConstants.WebsiteUrlMinLength)]
        [MaxLength(ValidationConstants.WebsiteUrlMaxLength)]
        [RegularExpression(ValidationConstants.WebsiteUrlRegEx)]
        public string WebsiteUrl { get; set; }

        [MinLength(ValidationConstants.OccupationMinLength)]
        [MaxLength(ValidationConstants.OccupationMaxLength)]
        public string Occupation { get; set; }

        [MinLength(ValidationConstants.InterestsMinLength)]
        [MaxLength(ValidationConstants.InterestsMaxLength)]
        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.AboutMeMinLength)]
        [MaxLength(ValidationConstants.AboutMeMaxLength)]
        public string AboutMe { get; set; }

        [MinLength(ValidationConstants.CountryMinLength)]
        [MaxLength(ValidationConstants.CountryMaxLength)]
        public string Country { get; set; }

        [MinLength(ValidationConstants.CityMinLength)]
        [MaxLength(ValidationConstants.CityMaxLength)]
        public string City { get; set; }

        [RegularExpression(ValidationConstants.GitHubProfileRegEx)]
        public string GitHubProfile { get; set; }

        [RegularExpression(ValidationConstants.StackOverflowProfileRegEx)]
        public string StackOverflowProfile { get; set; }

        [RegularExpression(ValidationConstants.LinkedInProfileRegEx)]
        public string LinkedInProfile { get; set; }

        [RegularExpression(ValidationConstants.FacebookProfileRegEx)]
        public string FacebookProfile { get; set; }

        [RegularExpression(ValidationConstants.TwitterProfileRegEx)]
        public string TwitterProfile { get; set; }

        [MinLength(ValidationConstants.SkypeProfileMinLength)]
        [MaxLength(ValidationConstants.SkypeProfileMaxLength)]
        [RegularExpression(ValidationConstants.SkypeProfileRegEx)]
        public string SkypeProfile { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty("Receiver")]
        public virtual ICollection<Notification> ReceivedNotifications { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Notification> SentNotifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}