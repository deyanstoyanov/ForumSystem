namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels;
    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Sections;

    public class SidebarController : BaseController
    {
        public SidebarController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public ActionResult Index()
        {
            var categories =
                this.Data.Categories.All()
                    .OrderByDescending(c => c.Posts.Count)
                    .ProjectTo<CategoryConciseViewModel>()
                    .Take(10)
                    .ToList();
            var sections = this.Data.Sections.All().ProjectTo<SectionViewModel>().ToList();

            var model = new SidebarViewModel { Categories = categories, Sections = sections };

            return this.PartialView("_SidebarPartial", model);
        }
    }
}