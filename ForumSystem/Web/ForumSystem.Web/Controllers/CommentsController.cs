namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.ViewModels.Comment;

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
                    .ProjectTo<CommentViewModel>();

            return this.PartialView(comments);
        }
    }
}