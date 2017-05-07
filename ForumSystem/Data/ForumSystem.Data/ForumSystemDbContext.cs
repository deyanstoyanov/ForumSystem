namespace ForumSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ForumSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumSystemDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<Section> Sections { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<PostReport> PostReports { get; set; }

        public virtual IDbSet<AnswerReport> AnswerReports { get; set; }

        public virtual IDbSet<CommentReport> CommentReports { get; set; }

        public virtual IDbSet<PostLike> PostLikes { get; set; }

        public virtual IDbSet<AnswerLike> AnswerLikes { get; set; }

        public virtual IDbSet<CommentLike> CommentLikes { get; set; }

        public virtual IDbSet<PostUpdate> PostUpdates { get; set; }

        public virtual IDbSet<AnswerUpdate> AnswerUpdates { get; set; }

        public virtual IDbSet<CommentUpdate> CommentUpdates { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public static ForumSystemDbContext Create()
        {
            return new ForumSystemDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)))
                )
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}