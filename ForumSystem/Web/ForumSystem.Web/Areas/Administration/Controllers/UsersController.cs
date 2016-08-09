namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class UsersController : AdministrationController
    {
        public UsersController(IForumSystemData data)
            : base(data)
        {
        }
    }
}