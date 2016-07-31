namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class PostsController : ModeratorController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}