namespace ForumSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.InputModels.CommentReports;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class CommentReportsController : BaseController
    {
        public CommentReportsController(IForumSystemData data)
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

            var comment = this.Data.Comments.GetById(id);
            if (comment == null || comment.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (comment.AuthorId == userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inputModel = new CommentReportInputModel { CommentId = comment.Id };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentReportInputModel inputModel)
        {
            if (inputModel != null && this.ModelState.IsValid)
            {
                var userid = this.User.Identity.GetUserId();
                var report = new CommentReport
                                 {
                                     AuthorId = userid, 
                                     CommentId = inputModel.CommentId, 
                                     Description = inputModel.Description
                                 };

                this.Data.CommentReports.Add(report);
                this.Data.SaveChanges();

                return this.JsonSuccess("Successfully created report.");
            }

            return this.JsonError("Description is required");
        }
    }
}