namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;

    public class PostUpdatesController : BaseController
    {
        public PostUpdatesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}