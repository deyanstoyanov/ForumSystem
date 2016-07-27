namespace ForumSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.InputModels.Answers;
    using ForumSystem.Web.ViewModels.Answer;

    using Microsoft.AspNet.Identity;

    public class AnswersController : BaseController
    {
        public AnswersController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
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

            var inputModel = new AnswerInputModel { PostId = post.Id, Post = post.Title };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Content")] AnswerInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var answer = new Answer
                                 {
                                     Content = input.Content,
                                     PostId = input.PostId,
                                     AuthorId = userId
                                 };

                this.Data.Answers.Add(answer);
                this.Data.SaveChanges();

                var viewModel = Mapper.Map<AnswerViewModel>(answer);

                return this.PartialView("_AnswerDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = this.User.Identity.GetUserId();
            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.HttpNotFound();
            }

            if (answer.AuthorId != userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new AnswerEditModel { Id = answer.Id, Content = answer.Content };

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] AnswerEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var answer = this.Data.Answers.GetById(model.Id);

                if (answer.AuthorId != userId)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                answer.Content = model.Content;

                this.Data.Answers.Update(answer);
                this.Data.SaveChanges();

                var viewModel = Mapper.Map<AnswerViewModel>(answer);

                return this.PartialView("_AnswerDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }
    }
}