namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.InputModels.AnswerLikes;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class AnswerLikesController : BaseController
    {
        public AnswerLikesController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Like(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);
            if (answer == null || answer.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var model = new AnswerLikeInputModel { AnswerId = answer.Id };

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(AnswerLikeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var isLiked =
                    this.Data.AnswerLikes.All()
                        .Any(l => l.UserId == userId && l.AnswerId == input.AnswerId && !l.IsDeleted);

                if (isLiked)
                {
                    return this.Dislike(input);
                }

                var newLike = new AnswerLike { UserId = userId, AnswerId = input.AnswerId };

                this.Data.AnswerLikes.Add(newLike);
                this.Data.SaveChanges();

                var likesCount = this.Data.AnswerLikes.All().Count(a => a.AnswerId == input.AnswerId);

                return this.JsonSuccess(likesCount);
            }

            return this.JsonError("Answer id is required");
        }

        private ActionResult Dislike(AnswerLikeInputModel input)
        {
            var userId = this.User.Identity.GetUserId();
            var like =
                this.Data.AnswerLikes.All()
                    .FirstOrDefault(l => l.UserId == userId && l.AnswerId == input.AnswerId && !l.IsDeleted);
            if (like != null)
            {
                this.Data.AnswerLikes.Delete(like.Id);
            }

            this.Data.SaveChanges();

            var likesCount = this.Data.AnswerLikes.All().Count(a => a.AnswerId == input.AnswerId);

            return this.JsonSuccess(likesCount);
        }
    }
}