namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class AnswersController : BaseController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }
    }
}