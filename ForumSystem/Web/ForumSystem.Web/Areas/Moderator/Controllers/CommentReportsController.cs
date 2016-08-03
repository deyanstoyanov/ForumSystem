namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.ViewModels.CommentReports;

    public class CommentReportsController : ModeratorController
    {
        public CommentReportsController(IForumSystemData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var reports = this.Data.CommentReports.All().ProjectTo<CommentReportViewModel>();

            return this.View(reports);
        }
    }
}