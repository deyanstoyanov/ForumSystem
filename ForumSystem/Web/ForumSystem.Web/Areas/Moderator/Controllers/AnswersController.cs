namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;
    using ForumSystem.Web.Areas.Moderator.InputModels.Answers;
    using ForumSystem.Web.ViewModels.Answers;

    using Microsoft.AspNet.Identity;

    public class AnswersController : ModeratorController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
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

            var model = new AnswerEditModel { Id = answer.Id, Content = answer.Content };

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var answer = this.Data.Answers.GetById(model.Id);

                answer.Content = model.Content;

                this.Data.Answers.Update(answer);
                this.Data.SaveChanges();

                if (model.Reason != null)
                {
                    var answerUpdate = new AnswerUpdate
                                         {
                                             AuthorId = userId, 
                                             AnswerId = answer.Id,
                        Reason = model.Reason
                    };

                    this.Data.AnswerUpdates.Add(answerUpdate);
                    this.Data.SaveChanges();
                }

                var viewModel = Mapper.Map<AnswerViewModel>(answer);
                viewModel.IsUpdating = true;

                return this.PartialView("_AnswerDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }
    }
}