namespace ForumSystem.Data.UnitOfWork
{
    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Models;

    public interface IForumSystemData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Section> Sections { get; }

        IRepository<Category> Caregories { get; }

        IRepository<Post> Posts { get; }

        IRepository<Answer> Answers { get; }

        IRepository<Comment> Comments { get; }

        void SaveChanges();
    }
}