namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class PostReportsController : ModeratorController
    {
        public PostReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}