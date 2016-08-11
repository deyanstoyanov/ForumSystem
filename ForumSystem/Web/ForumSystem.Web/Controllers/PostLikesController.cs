namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.InputModels.PostLikes;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class PostLikesController : BaseController
    {
        public PostLikesController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Like(int? id)
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
            if (post.AuthorId == userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PostLikeInputModel { PostId = post.Id };

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(PostLikeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var isLiked =
                    this.Data.PostLikes.All().Any(l => l.UserId == userId && l.PostId == input.PostId && !l.IsDeleted);

                if (isLiked)
                {
                    return this.Dislike(input);
                }

                var newLike = new PostLike { UserId = userId, PostId = input.PostId };

                this.Data.PostLikes.Add(newLike);
                this.Data.SaveChanges();

                var likesCount = this.Data.PostLikes.All().Count(p => p.PostId == input.PostId);

                return this.JsonSuccess(likesCount);
            }

            return this.JsonError("Post id is required");
        }

        private ActionResult Dislike(PostLikeInputModel input)
        {
            var userId = this.User.Identity.GetUserId();
            var like =
                this.Data.PostLikes.All()
                    .FirstOrDefault(l => l.UserId == userId && l.PostId == input.PostId && !l.IsDeleted);
            if (like != null)
            {
                this.Data.PostLikes.Delete(like.Id);
            }

            this.Data.SaveChanges();

            var likesCount = this.Data.PostLikes.All().Count(p => p.PostId == input.PostId);

            return this.JsonSuccess(likesCount);
        }
    }
}