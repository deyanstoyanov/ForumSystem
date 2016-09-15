namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.InputModels.Posts;
    using ForumSystem.Web.Areas.Administration.ViewModels.Posts;
    using ForumSystem.Web.Areas.Moderator.ViewModels.Categories;

    using Microsoft.AspNet.Identity;

    public class PostsController : AdministrationController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var posts =
                this.Data.Posts.AllWithDeleted().OrderByDescending(p => p.CreatedOn).ProjectTo<PostViewModel>().ToList();

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

            var categories = this.Data.Categories.All().ProjectTo<CategoryConciseViewModel>().ToList();
            var model = new PostEditModel
                            {
                                Id = post.Id, 
                                Title = post.Title, 
                                Content = post.Content, 
                                IsDeleted = post.IsDeleted, 
                                CategoryId = post.CategoryId, 
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
                var userId = this.User.Identity.GetUserId();

                post.Title = model.Title;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.IsDeleted = model.IsDeleted;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                if (model.Reason != null)
                {
                    var postUpdate = new PostUpdate { AuthorId = userId, PostId = post.Id, Reason = model.Reason };

                    this.Data.PostUpdates.Add(postUpdate);
                    this.Data.SaveChanges();
                }

                return this.RedirectToAction("All");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
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

            var model = Mapper.Map<PostViewModel>(post);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = this.Data.Posts.GetById(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Posts.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}