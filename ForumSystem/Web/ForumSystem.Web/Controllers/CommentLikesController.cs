namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.InputModels.CommentLikes;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class CommentLikesController : BaseController
    {
        public CommentLikesController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult Like(int? id)
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
            var isLiked = this.Data.CommentLikes.All().Any(l => l.UserId == userId && l.CommentId == id && !l.IsDeleted);
            var likesCount = this.Data.CommentLikes.All().Count(a => a.CommentId == id);

            var model = new CommentLikeInputModel
                            {
                                CommentId = comment.Id, 
                                CommentAuthorId = comment.AuthorId, 
                                IsLiked = isLiked, 
                                LikesCount = likesCount
                            };

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(CommentLikeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.Data.Comments.GetById(input.CommentId);
                if (comment == null || comment.IsDeleted)
                {
                    return this.HttpNotFound();
                }

                var userId = this.User.Identity.GetUserId();
                if (comment.AuthorId == userId)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var isLiked =
                    this.Data.CommentLikes.All()
                        .Any(l => l.UserId == userId && l.CommentId == input.CommentId && !l.IsDeleted);

                if (isLiked)
                {
                    return this.Dislike(input);
                }

                var newLike = new CommentLike { UserId = userId, CommentId = input.CommentId };

                this.Data.CommentLikes.Add(newLike);
                this.Data.SaveChanges();

                var likesCount = this.Data.CommentLikes.All().Count(a => a.CommentId == input.CommentId);

                input.IsLiked = true;
                input.LikesCount = likesCount;

                var newNotification = new Notification
                                          {
                                              NotificationType = NotificationType.LikePost,
                                              ItemId = comment.Id,
                                              SenderId = userId,
                                              ReceiverId = comment.AuthorId
                                          };

                this.Data.Notifications.Add(newNotification);
                this.Data.SaveChanges();

                this.UpdateNotificationsCount(comment.Author);

                return this.PartialView(input);
            }

            return this.JsonError("Comment id is required");
        }

        private ActionResult Dislike(CommentLikeInputModel input)
        {
            var userId = this.User.Identity.GetUserId();
            var like =
                this.Data.CommentLikes.All()
                    .FirstOrDefault(l => l.UserId == userId && l.CommentId == input.CommentId && !l.IsDeleted);
            if (like != null)
            {
                this.Data.CommentLikes.Delete(like.Id);
                this.Data.SaveChanges();
            }

            var likesCount = this.Data.CommentLikes.All().Count(a => a.CommentId == input.CommentId);

            input.IsLiked = false;
            input.LikesCount = likesCount;

            return this.PartialView("Like", input);
        }
    }
}