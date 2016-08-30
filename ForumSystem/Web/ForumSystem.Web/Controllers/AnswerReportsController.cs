namespace ForumSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.InputModels.AnswerReports;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class AnswerReportsController : BaseController
    {
        public AnswerReportsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);
            if (answer == null || answer.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userid = this.User.Identity.GetUserId();
            if (answer.AuthorId == userid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inputModel = new AnswerReportInputModel { AnswerId = answer.Id };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerReportInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var report = new AnswerReport
                                 {
                                     AuthorId = userId, 
                                     AnswerId = input.AnswerId, 
                                     Description = input.Description
                                 };

                this.Data.AnswerReports.Add(report);
                this.Data.SaveChanges();

                return this.JsonSuccess("Successfully created report.");
            }

            return this.JsonError("Description is required");
        }
    }
}