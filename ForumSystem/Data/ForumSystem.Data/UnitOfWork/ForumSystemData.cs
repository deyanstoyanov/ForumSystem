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

    internal class ForumSystemData : IForumSystemData
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

        public IRepository<ApplicationUser> Users => this.GetRepository<ApplicationUser>();

        public IRepository<Section> Sections => this.GetRepository<Section>();

        public IRepository<Category> Caregories => this.GetRepository<Category>();

        public IRepository<Post> Posts => this.GetRepository<Post>();

        public IRepository<Answer> Answers => this.GetRepository<Answer>();

        public IRepository<Comment> Comments => this.GetRepository<Comment>();

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}