namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class CommentLikesController : BaseController
    {
        public CommentLikesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}