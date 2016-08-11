namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class LastActivitiesController : BaseController
    {
        public LastActivitiesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}