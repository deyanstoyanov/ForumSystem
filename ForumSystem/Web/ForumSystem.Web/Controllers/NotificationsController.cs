namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;

    public class NotificationsController : BaseController
    {
        public NotificationsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}