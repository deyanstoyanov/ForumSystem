namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;
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
            var reports =
                this.Data.AnswerReports.All().OrderBy(r => r.CreatedOn).ProjectTo<AnswerReportViewModel>().ToList();

            return this.View(reports);
        }

        [HttpGet]
        public ActionResult GetAllById(int? id)
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

            var reports =
                this.Data.AnswerReports.All()
                    .Where(r => r.AnswerId == id)
                    .OrderBy(r => r.CreatedOn)
                    .ProjectTo<AnswerReportViewModel>()
                    .ToList();

            return this.PartialView(reports);
        }

        [HttpGet]
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

            var model = Mapper.Map<AnswerReportViewModel>(report);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
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