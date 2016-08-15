namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Posts;
    using ForumSystem.Web.ViewModels.Sections;

    public class SidebarController : BaseController
    {
        public SidebarController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 10 * 60)]
        public ActionResult AllSections()
        {
            var sections = this.Data.Sections.All().ProjectTo<SectionViewModel>().ToList();

            return this.PartialView(sections);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 10 * 60)]
        public ActionResult TopCategories()
        {
            var categories =
                this.Data.Categories.All()
                    .Where(c => c.Posts.Count(p => !p.IsDeleted) > 0)
                    .OrderByDescending(c => c.Posts.Count)
                    .ProjectTo<CategoryConciseViewModel>()
                    .Take(10)
                    .ToList();

            return this.PartialView(categories);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 30)]
        public ActionResult LastPosts()
        {
            var lastPosts =
                this.Data.Posts.All()
                    .OrderByDescending(p => p.CreatedOn)
                    .ProjectTo<PostConciseViewModel>()
                    .Take(5)
                    .ToList();

            return this.PartialView(lastPosts);
        }
    }
}