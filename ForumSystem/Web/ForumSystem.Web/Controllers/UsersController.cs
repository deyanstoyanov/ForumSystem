namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;

    public class UsersController : BaseController
    {
        public UsersController(IForumSystemData data)
            : base(data)
        {
        }
    }
}