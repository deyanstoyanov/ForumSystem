namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;

    public class CommentsController : ModeratorController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}