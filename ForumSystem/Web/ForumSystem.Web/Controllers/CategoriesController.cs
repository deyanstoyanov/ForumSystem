namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Category;
    using ForumSystem.Web.ViewModels.Post;

    using PagedList;

    public class CategoriesController : BaseController
    {
        private const int PostsPerPageDefaultValue = 12;

        public CategoriesController(IForumSystemData data)
            : base(data)
        {
        }

        // GET: Categories/Details/5
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
                    .Where(p => p.CategoryId == id)
                    .OrderByDescending(p => p.CreatedOn)
                    .ProjectTo<PostConciseViewModel>();
            var model = posts.ToPagedList(pagenumber, PostsPerPageDefaultValue);

            return this.PartialView("_CategoryPostsPartial", model);
        }
    }
}