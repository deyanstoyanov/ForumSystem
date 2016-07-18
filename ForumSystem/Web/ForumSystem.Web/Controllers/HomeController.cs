namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Data.UnitOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IForumSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}