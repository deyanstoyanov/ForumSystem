namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using ForumSystem.Data.UnitOfWork;

    public class CategoriesController : AdministrationController
    {
        public CategoriesController(IForumSystemData data)
            : base(data)
        {
        }
    }
}