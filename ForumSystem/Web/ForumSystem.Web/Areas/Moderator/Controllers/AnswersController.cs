namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;

    public class AnswersController : ModeratorController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }
    }
}