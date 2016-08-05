namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
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
            var reports = this.Data.CommentReports.All().OrderBy(r => r.CreatedOn).ProjectTo<CommentReportViewModel>();

            return this.View(reports);
        }

        [HttpGet]
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

            var model = Mapper.Map<CommentReportViewModel>(report);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
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