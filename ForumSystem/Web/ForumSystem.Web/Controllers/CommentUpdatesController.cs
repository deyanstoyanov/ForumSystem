namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.CommentUpdates;

    public class CommentUpdatesController : BaseController
    {
        public CommentUpdatesController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult GetById(int? id)
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

            var model =
                this.Data.CommentUpdates.All()
                    .Where(u => u.CommentId == id)
                    .OrderByDescending(u => u.CreatedOn)
                    .ProjectTo<CommentUpdateViewModel>()
                    .FirstOrDefault();

            if (model == null)
            {
                return new EmptyResult();
            }

            return this.PartialView(model);
        }
    }
}