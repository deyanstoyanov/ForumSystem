namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;

    public class PostsController : AdministrationController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}