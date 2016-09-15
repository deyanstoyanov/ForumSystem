namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.InputModels.Answers;
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
                    .ProjectTo<AnswerViewModel>()
                    .ToList();

            return this.View(posts);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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

            var model = new AnswerEditModel { Id = answer.Id, Content = answer.Content, IsDeleted = answer.IsDeleted };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var answer = this.Data.Answers.GetById(model.Id);

                answer.Content = model.Content;
                answer.IsDeleted = model.IsDeleted;

                this.Data.Answers.Update(answer);
                this.Data.SaveChanges();

                return this.RedirectToAction("All");
            }

            return this.View(model);
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