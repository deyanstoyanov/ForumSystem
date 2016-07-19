namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Post;

    public class PostsController : BaseController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.GetById(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.Data.Posts.All()
                .Where(p => p.Id == id)
                .ProjectTo<PostViewModel>()
                .FirstOrDefault();

            return this.View(viewModel);
        }
    }
}