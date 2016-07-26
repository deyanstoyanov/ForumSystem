namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class CommentsController : BaseController
    {
        protected CommentsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}