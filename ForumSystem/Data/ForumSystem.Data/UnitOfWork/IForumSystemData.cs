namespace ForumSystem.Data.UnitOfWork
{
    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Models;

    public interface IForumSystemData
    {
        IDeletableEntityRepository<ApplicationUser> Users { get; }

        IDeletableEntityRepository<Section> Sections { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<Post> Posts { get; }

        IDeletableEntityRepository<Answer> Answers { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<PostReport> PostReports { get; }

        IDeletableEntityRepository<AnswerReport> AnswerReports { get; }

        IDeletableEntityRepository<CommentReport> CommentReports { get; }

        IDeletableEntityRepository<PostLike> PostLikes { get; }

        IDeletableEntityRepository<AnswerLike> AnswerLikes { get; }

        IDeletableEntityRepository<CommentLike> CommentLikes { get; }

        IDeletableEntityRepository<PostUpdate> PostUpdates { get; }

        IDeletableEntityRepository<AnswerUpdate> AnswerUpdates { get; }

        IDeletableEntityRepository<CommentUpdate> CommentUpdates { get; }

        IDeletableEntityRepository<Notification> Notifications { get; }

        void SaveChanges();
    }
}