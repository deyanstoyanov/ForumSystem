namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.Infrastructure.Extensions;
    using ForumSystem.Web.InputModels.Posts;
    using ForumSystem.Web.ViewModels.Answers;
    using ForumSystem.Web.ViewModels.Posts;

    using Microsoft.AspNet.Identity;

    using PagedList;

    public class PostsController : BaseController
    {
        private const int AnswersPerPageDefaultValue = 10;

        public PostsController(IForumSystemData data)
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

            var post = this.Data.Posts.GetById(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = 
                this.Data.Posts.All()
                    .Where(p => p.Id == id)
                    .ProjectTo<PostViewModel>()
                    .FirstOrDefault();

            if (viewModel != null && post.IsLocked)
            {
                viewModel.LockedBy =
                    this.Data.Users.All()
                        .Where(u => u.Id == post.LockedById)
                        .Select(u => u.UserName)
                        .FirstOrDefault();
            }

            post.Views++;

            this.Data.Posts.Update(post);
            this.Data.SaveChanges();

            return this.View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult Answers(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var posts = this.Data.Posts.GetById(id);
            if (posts == null)
            {
                return this.HttpNotFound();
            }

            var pageNumber = page ?? 1;

            var answers =
                this.Data.Answers.All()
                    .Where(a => a.PostId == id)
                    .OrderBy(x => x.CreatedOn)
                    .ProjectTo<AnswerViewModel>()
                    .ToList();

            var model = answers.ToPagedList(pageNumber, AnswersPerPageDefaultValue);

            return this.PartialView("_PostAnswersPartial", model);
        }

        [ChildActionOnly]
        public ActionResult GetById(int id)
        {
            var post = this.Data.Posts.GetById(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model =
                this.Data.Posts.All()
                    .Where(p => p.Id == id)
                    .ProjectTo<PostViewModel>()
                    .FirstOrDefault();

            return this.PartialView("_PostDetailPartial", model);
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
        public ActionResult Create(PostInputModel input)
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

                post.LastActivity = post.CreatedOn;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
            }

            return this.View(input);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = this.User.Identity.GetUserId();
            var post = this.Data.Posts.GetById(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            if (post.AuthorId != userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PostEditModel { Id = post.Id, Title = post.Title, Content = post.Content };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var post = this.Data.Posts.GetById(model.Id);

                if (post.AuthorId != userId)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                post.Title = model.Title;
                post.Content = model.Content;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = Mapper.Map<PostViewModel>(post);

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.Data.Posts.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Categories", new { area = string.Empty, id = post.CategoryId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Lock(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PostLockInputModel { PostId = post.Id };

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Lock(PostLockInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var post = this.Data.Posts.GetById(input.PostId);
                if (post == null || post.IsDeleted)
                {
                    return this.HttpNotFound();
                }

                var userId = this.User.Identity.GetUserId();
                if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                post.LockReason = input.LockReason;
                post.LockedById = userId;
                post.IsLocked = true;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
            }

            return this.JsonError("Reason is required");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Unlock(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = Mapper.Map<PostViewModel>(post);

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Unlock(int id)
        {
            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            post.LockedById = null;
            post.LockReason = null;
            post.IsLocked = false;

            this.Data.Posts.Update(post);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = post.Id });
        }
    }
}