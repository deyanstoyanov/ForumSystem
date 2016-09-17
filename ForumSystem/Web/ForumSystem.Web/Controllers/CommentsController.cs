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
    using ForumSystem.Web.InputModels.Comments;
    using ForumSystem.Web.ViewModels.Comments;

    using Microsoft.AspNet.Identity;

    public class CommentsController : BaseController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult All(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.HttpNotFound();
            }

            var comments =
                this.Data.Comments.All()
                    .Where(c => c.AnswerId == id)
                    .OrderBy(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>()
                    .ToList();

            return this.PartialView(comments);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Data.Comments.All().Where(c => c.Id == id).ProjectTo<CommentViewModel>().FirstOrDefault();

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);
            if (answer == null || answer.IsDeleted || answer.Post.IsLocked)
            {
                return this.HttpNotFound();
            }

            var inputModel = new CommentInputModel { AnswerId = answer.Id };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var comment = new Comment
                                  {
                                      AnswerId = input.AnswerId,
                                      Content = input.Content,
                                      AuthorId = userId
                                  };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var postId = this.Data.Answers.GetById(comment.AnswerId).PostId;
                var post = this.Data.Posts.GetById(postId);

                post.LastActivity = comment.CreatedOn;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                var answer = this.Data.Answers.GetById(comment.AnswerId);
                if (answer.AuthorId != userId)
                {
                    var newNotification = new Notification
                                              {
                                                  NotificationType = NotificationType.CommentAnswer,
                                                  ItemId = comment.Id,
                                                  SenderId = userId,
                                                  ReceiverId = answer.AuthorId
                                              };

                    this.Data.Notifications.Add(newNotification);
                    this.Data.SaveChanges();

                    this.UpdateNotificationsCount(answer.Author);
                }

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return this.PartialView("_CommentDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
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
            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CommentEditModel { Id = comment.Id, Content = comment.Content };

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var comment = this.Data.Comments.GetById(model.Id);

                if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                comment.Content = model.Content;

                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return this.PartialView("_CommentDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data.Comments.GetById(id);
            if (comment == null || comment.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = Mapper.Map<CommentViewModel>(comment);

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var comment = this.Data.Comments.GetById(id);
            if (comment == null || comment.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.Data.Comments.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", "Posts", new { area = string.Empty, id = comment.Answer.PostId });
        }
    }
}