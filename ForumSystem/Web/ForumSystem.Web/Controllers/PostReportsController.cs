namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class PostReportsController : BaseController
    {
        public PostReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}