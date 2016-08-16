namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.ViewModels.Answers;

    public class AnswersController : AdministrationController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var posts =
                this.Data.Answers.AllWithDeleted()
                    .OrderByDescending(p => p.CreatedOn)
                    .ProjectTo<AnswerViewModel>();

            return this.View(posts);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
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

            var model = Mapper.Map<AnswerViewModel>(answer);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Answers.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}