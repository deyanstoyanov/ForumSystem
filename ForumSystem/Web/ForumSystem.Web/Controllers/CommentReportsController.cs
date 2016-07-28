namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class CommentReportsController : BaseController
    {
        public CommentReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}