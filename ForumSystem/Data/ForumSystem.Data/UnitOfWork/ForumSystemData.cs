namespace ForumSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ForumSystemData : IForumSystemData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        private IUserStore<ApplicationUser> userStore;

        public ForumSystemData(DbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IUserStore<ApplicationUser> UserStore
            => this.userStore ?? (this.userStore = new UserStore<ApplicationUser>(this.dbContext));

        public IDeletableEntityRepository<ApplicationUser> Users => this.GetRepository<ApplicationUser>();

        public IDeletableEntityRepository<Section> Sections => this.GetRepository<Section>();

        public IDeletableEntityRepository<Category> Categories => this.GetRepository<Category>();

        public IDeletableEntityRepository<Post> Posts => this.GetRepository<Post>();

        public IDeletableEntityRepository<Answer> Answers => this.GetRepository<Answer>();

        public IDeletableEntityRepository<Comment> Comments => this.GetRepository<Comment>();

        public IDeletableEntityRepository<PostReport> PostReports => this.GetRepository<PostReport>();

        public IDeletableEntityRepository<AnswerReport> AnswerReports => this.GetRepository<AnswerReport>();

        public IDeletableEntityRepository<CommentReport> CommentReports => this.GetRepository<CommentReport>();

        public IDeletableEntityRepository<PostLike> PostLikes => this.GetRepository<PostLike>();

        public IDeletableEntityRepository<AnswerLike> AnswerLikes => this.GetRepository<AnswerLike>();

        public IDeletableEntityRepository<CommentLike> CommentLikes => this.GetRepository<CommentLike>();

        public IDeletableEntityRepository<PostUpdate> PostUpdates => this.GetRepository<PostUpdate>();

        public IDeletableEntityRepository<AnswerUpdate> AnswerUpdates => this.GetRepository<AnswerUpdate>();

        public IDeletableEntityRepository<CommentUpdate> CommentUpdates => this.GetRepository<CommentUpdate>();

        public IDeletableEntityRepository<Notification> Notifications => this.GetRepository<Notification>();

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.dbContext));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}