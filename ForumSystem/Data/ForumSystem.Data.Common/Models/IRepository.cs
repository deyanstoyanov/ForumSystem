namespace ForumSystem.Data.Common.Models
{
    using System;
    using System.Linq;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        T Add(T entity);

        T Update(T entity);

        T Delete(object id);

        void Delete(T entity);

        int SaveChanges();
    }
}