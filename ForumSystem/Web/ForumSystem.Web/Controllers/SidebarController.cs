namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class SidebarController : BaseController
    {
        public SidebarController(IForumSystemData data)
            : base(data)
        {
        }
    }
}