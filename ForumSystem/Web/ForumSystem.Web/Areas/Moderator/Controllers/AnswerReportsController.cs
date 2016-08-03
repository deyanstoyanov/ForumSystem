namespace ForumSystem.Web.Areas.Moderator.Controllers
{
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
    }
}