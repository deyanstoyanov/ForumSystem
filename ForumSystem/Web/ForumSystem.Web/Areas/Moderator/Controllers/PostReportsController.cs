namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
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
            var reports = this.Data.PostReports.All().ProjectTo<PostReportViewModel>();

            return this.View(reports);
        }
    }
}