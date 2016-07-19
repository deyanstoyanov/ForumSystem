namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.InputModels.Posts;
    using ForumSystem.Web.ViewModels.Post;

    using Microsoft.AspNet.Identity;

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

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? id)
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

            var inputModel = new PostInputModel { CategoryId = category.Id, Category = category.Title };

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,CategoryId,Category")] PostInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var post = new Post
                               {
                                   Title = input.Title, 
                                   Content = input.Content, 
                                   AuthorId = userId, 
                                   CategoryId = input.CategoryId
                               };

                this.Data.Posts.Add(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
            }

            return this.View(input);
        }
    }
}