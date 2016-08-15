namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.LastActivities;

    public class LastActivitiesController : BaseController
    {
        public LastActivitiesController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Post(int? id)
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

            var lastAnswer =
                this.Data.Answers.All()
                    .Where(a => a.PostId == id)
                    .OrderByDescending(a => a.CreatedOn)
                    .FirstOrDefault();

            var lastComment =
                this.Data.Comments.All()
                    .Where(c => c.Answer.PostId == id)
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault();

            if (lastAnswer == null && lastComment == null)
            {
                return this.HttpNotFound();
            }

            var model = this.GetLastActivity(lastAnswer, lastComment);

            return this.PartialView("_PostLastActivityPartial", model);
        }

        private PostLastActivityViewModel GetLastActivity(Answer lastAnswer, Comment lastComment)
        {
            var model = new PostLastActivityViewModel();

            if (lastAnswer == null)
            {
                model = this.GetComment(lastComment);

                return model;
            }

            if (lastComment == null)
            {
                model = this.GetAnswer(lastAnswer);

                return model;
            }

            var checkDate = DateTime.Compare(lastAnswer.CreatedOn, lastComment.CreatedOn);

            if (checkDate < 0)
            {
                model = this.GetComment(lastComment);
            }
            else if (checkDate > 0)
            {
                model = this.GetAnswer(lastAnswer);
            }
            else
            {
                model = this.GetAnswer(lastAnswer);
            }

            return model;
        }

        private PostLastActivityViewModel GetComment(Comment lastComment)
        {
            var model = new PostLastActivityViewModel
                            {
                                AnswerId = lastComment.AnswerId, 
                                AuthorId = lastComment.AuthorId, 
                                Author = lastComment.Author.UserName, 
                                CreatedOn = lastComment.CreatedOn
                            };

            return model;
        }

        private PostLastActivityViewModel GetAnswer(Answer lastAnswer)
        {
            var model = new PostLastActivityViewModel
                            {
                                AnswerId = lastAnswer.Id, 
                                AuthorId = lastAnswer.AuthorId, 
                                Author = lastAnswer.Author.UserName, 
                                CreatedOn = lastAnswer.CreatedOn
                            };

            return model;
        }

        [ChildActionOnly]
        public ActionResult CategoryAllReplies(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.Data.Categories.GetById(id);
            if (category == null || category.IsDeleted)
            {
                return this.HttpNotFound();
            }

            var answers = this.Data.Answers.All().Count(a => a.Post.CategoryId == id && !a.IsDeleted);
            var comments = this.Data.Comments.All().Count(c => c.Answer.Post.CategoryId == id && !c.IsDeleted);
            var allReplies = answers + comments;

            var model = new CategoryAllRepliesViewModel() { AllReplies = allReplies };

            return this.PartialView(model);
        }
    }
}