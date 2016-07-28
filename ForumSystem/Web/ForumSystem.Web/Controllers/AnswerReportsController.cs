namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class AnswerReportsController : BaseController
    {
        public AnswerReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}