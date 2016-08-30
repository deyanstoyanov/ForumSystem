namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;
    using ForumSystem.Web.Areas.Moderator.InputModels.Posts;
    using ForumSystem.Web.Areas.Moderator.ViewModels.Categories;

    public class PostsController : ModeratorController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
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
                                CategoryId = post.CategoryId, 
                                Categories = new SelectList(categories, "Id", "Title", post.CategoryId)
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                var post = this.Data.Posts.GetById(model.Id);

                post.Title = model.Title;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
            }

            return this.View(model);
        }
    }
}