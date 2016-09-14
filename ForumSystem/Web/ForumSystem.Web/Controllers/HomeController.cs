namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Home;
    using ForumSystem.Web.ViewModels.Sections;

    public class HomeController : BaseController
    {
        public HomeController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var sections = this.Data.Sections.All().ProjectTo<SectionViewModel>().ToList();
            var viewModel = new IndexPageViewModel { Sections = sections };

            return this.View(viewModel);
        }
    }
}