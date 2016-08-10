namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    [Authorize]
    public class AnswerLikesController : BaseController
    {
        public AnswerLikesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}