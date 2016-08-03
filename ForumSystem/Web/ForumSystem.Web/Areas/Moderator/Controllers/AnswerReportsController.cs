namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class AnswerReportsController : ModeratorController
    {
        public AnswerReportsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}