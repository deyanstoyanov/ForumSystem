namespace ForumSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Posts = new HashSet<Post>();
            this.Answers = new HashSet<Answer>();
            this.Comments = new HashSet<Comment>();

            this.CreatedOn = DateTime.Now;
        }

        public string PictureUrl { get; set; }

        public string WebsiteUrl { get; set; }

        public string Occupation { get; set; }

        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 6, ErrorMessage = "{0} text must be between {2} and {1} symbols long.")]
        public string AboutMe { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string GitHubProfile { get; set; }

        public string StackOverflowProfile { get; set; }

        public string LinkedInProfile { get; set; }

        public string FacebookProfile { get; set; }

        public string TwitterProfile { get; set; }

        public string SkypeProfile { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}