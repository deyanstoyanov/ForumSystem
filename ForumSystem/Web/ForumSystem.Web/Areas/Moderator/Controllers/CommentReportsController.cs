namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Net;
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var report = this.Data.CommentReports.GetById(id);
            if (report == null)
            {
                return this.HttpNotFound();
            }

            this.Data.CommentReports.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}