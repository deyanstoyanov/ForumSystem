namespace ForumSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.InputModels.PostReports;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class PostReportsController : BaseController
    {
        public PostReportsController(IForumSystemData data)
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

            var post = this.Data.Posts.GetById(id);
            if (post == null || post.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var userId = this.User.Identity.GetUserId();
            if (post.AuthorId == userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inputModel = new PostReportInputModel { PostId = post.Id };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostReportInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var report = new PostReport
                                 {
                                     PostId = input.PostId, 
                                     AuthorId = userId, 
                                     Description = input.Description
                                 };

                this.Data.PostReports.Add(report);
                this.Data.SaveChanges();

                return this.JsonSuccess("Successfully created report.");
            }

            return this.JsonError("Description is required");
        }
    }
}