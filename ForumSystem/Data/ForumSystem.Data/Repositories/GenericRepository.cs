namespace ForumSystem.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using ForumSystem.Data.Common.Models;

    internal class GenericRepository<T> : IRepository<T>
        where T : class
    {
        public GenericRepository(DbContext dbContext)
        {
            this.Context = dbContext;
            this.EntitySet = dbContext.Set<T>();
        }

        protected DbContext Context { get; set; }

        public IDbSet<T> EntitySet { get; }

        public virtual IQueryable<T> All()
        {
            return this.EntitySet;
        }

        public virtual T GetById(object id)
        {
            return this.EntitySet.Find(id);
        }

        public virtual T Add(T entity)
        {
            return this.ChangeState(entity, EntityState.Added);
        }

        public virtual T Update(T entity)
        {
            return this.ChangeState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public virtual T Delete(object id)
        {
            var entity = this.GetById(id);
            this.Delete(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        private T ChangeState(T entity, EntityState state)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.EntitySet.Attach(entity);
            }

            entry.State = state;
            return entity;
        }
    }
}