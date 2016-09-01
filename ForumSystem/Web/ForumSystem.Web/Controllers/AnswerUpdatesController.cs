namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.AnswerUpdates;

    public class AnswerUpdatesController : BaseController
    {
        public AnswerUpdatesController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult GetById(int? id)
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

            var model =
                this.Data.AnswerUpdates.All()
                    .Where(u => u.AnswerId == id)
                    .OrderByDescending(u => u.CreatedOn)
                    .ProjectTo<AnswerUpdateViewModel>()
                    .FirstOrDefault();

            if (model == null)
            {
                return new EmptyResult();
            }

            return this.PartialView(model);
        }
    }
}