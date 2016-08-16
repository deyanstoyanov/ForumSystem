namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.ViewModels.Comments;

    public class CommentsController : AdministrationController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var comments =
                this.Data.Comments.AllWithDeleted()
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>();

            return this.View(comments);
        }
    }
}