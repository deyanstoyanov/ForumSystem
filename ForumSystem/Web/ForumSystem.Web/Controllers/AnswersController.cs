namespace ForumSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

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

                var model = new AnswerViewModel
                                {
                                    Id = answer.Id, 
                                    AuthorId = userId, 
                                    Author = this.User.Identity.GetUserName(), 
                                    AuthorPictureUrl = answer.Author.PictureUrl, 
                                    Content = input.Content, 
                                    PostId = input.PostId, 
                                    CreatedOn = answer.CreatedOn
                                };

                return this.PartialView("_AnswerDetailPartial", model);
            }

            return this.JsonError("Content is required");
        }
    }
}