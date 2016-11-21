namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Posts;

    using PagedList;

    public class CategoriesController : BaseController
    {
        private const int PostsPerPageDefaultValue = 10;

        public CategoriesController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            var viewModel =
                this.Data.Categories.All()
                    .Where(c => c.Id == id)
                    .ProjectTo<CategoryViewModel>()
                    .FirstOrDefault();

            return this.View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult Posts(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            var pagenumber = page ?? 1;
            var posts =
                this.Data.Posts.All()
                    .Where(p => p.CategoryId == id && !p.IsPinned)
                    .OrderByDescending(p => p.LastActivity)
                    .ThenByDescending(p => p.CreatedOn)
                    .ProjectTo<PostConciseViewModel>()
                    .ToList();

            var model = posts.ToPagedList(pagenumber, PostsPerPageDefaultValue);

            return this.PartialView("_CategoryPostsPartial", model);
        }

        [ChildActionOnly]
        public ActionResult PinnedPosts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.Data.Categories.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            var posts =
                this.Data.Posts.All()
                    .Where(p => p.CategoryId == id && p.IsPinned)
                    .OrderByDescending(p => p.IsPinned)
                    .ThenByDescending(p => p.LastActivity)
                    .ThenByDescending(p => p.CreatedOn)
                    .ProjectTo<PostConciseViewModel>()
                    .ToList();

            return this.PartialView("_CategoryPinnedPostsPartial", posts);
        }
    }
}