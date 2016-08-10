namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class PostLikesController : BaseController
    {
        public PostLikesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}