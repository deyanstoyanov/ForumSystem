namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class CommentReportsController : ModeratorController
    {
        public CommentReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}