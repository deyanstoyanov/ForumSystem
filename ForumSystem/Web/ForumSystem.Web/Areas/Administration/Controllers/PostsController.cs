namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.InputModels.Posts;
    using ForumSystem.Web.Areas.Administration.ViewModels.Posts;
    using ForumSystem.Web.Areas.Moderator.ViewModels.Categories;

    public class PostsController : AdministrationController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var posts = this.Data.Posts.AllWithDeleted().OrderByDescending(p => p.CreatedOn).ProjectTo<PostViewModel>();

            return this.View(posts);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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

            var categories = this.Data.Categories.All().ProjectTo<CategoryConciseViewModel>();
            var model = new PostEditModel
                            {
                                Id = post.Id, 
                                Title = post.Title, 
                                Content = post.Content, 
                                IsDeleted = post.IsDeleted, 
                                Categories = new SelectList(categories, "Id", "Title", post.CategoryId)
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var post = this.Data.Posts.GetById(model.Id);

                post.Title = model.Title;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.IsDeleted = model.IsDeleted;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(model);
        }
    }
}