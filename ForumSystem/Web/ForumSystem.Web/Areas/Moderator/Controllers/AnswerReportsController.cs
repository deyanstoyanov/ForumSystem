namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.ViewModels.AnswerReports;

    public class AnswerReportsController : ModeratorController
    {
        public AnswerReportsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var reports = this.Data.AnswerReports.All().ProjectTo<AnswerReportViewModel>();

            return this.View(reports);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var report = this.Data.AnswerReports.GetById(id);
            if (report == null)
            {
                return this.HttpNotFound();
            }

            this.Data.AnswerReports.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}