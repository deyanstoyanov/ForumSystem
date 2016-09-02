namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;

    public class CommentUpdatesController : BaseController
    {
        public CommentUpdatesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}