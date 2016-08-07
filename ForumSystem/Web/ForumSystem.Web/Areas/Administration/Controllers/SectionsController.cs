namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class SectionsController : AdministrationController
    {
        public SectionsController(IForumSystemData data)
            : base(data)
        {
        }
    }
}