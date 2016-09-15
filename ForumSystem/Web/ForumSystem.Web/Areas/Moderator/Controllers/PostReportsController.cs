namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;
    using ForumSystem.Web.Areas.Moderator.ViewModels.PostReports;

    public class PostReportsController : ModeratorController
    {
        public PostReportsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var reports =
                this.Data.PostReports.All().OrderBy(r => r.CreatedOn).ProjectTo<PostReportViewModel>().ToList();

            return this.View(reports);
        }

        [HttpGet]
        public ActionResult GetAllById(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.GetById(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var reports =
                this.Data.PostReports.All()
                    .Where(r => r.PostId == id)
                    .OrderBy(r => r.CreatedOn)
                    .ProjectTo<PostReportViewModel>()
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

            var report = this.Data.PostReports.GetById(id);
            if (report == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<PostReportViewModel>(report);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var report = this.Data.PostReports.GetById(id);
            if (report == null)
            {
                return this.HttpNotFound();
            }

            this.Data.PostReports.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}