namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;

    public class AnswersController : AdministrationController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }
    }
}