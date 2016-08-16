namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;

    public class CommentsController : AdministrationController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}