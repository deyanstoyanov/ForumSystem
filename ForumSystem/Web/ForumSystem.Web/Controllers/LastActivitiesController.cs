namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web.Mvc;

    using AutoMapper;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Answers;
    using ForumSystem.Web.ViewModels.Comments;
    using ForumSystem.Web.ViewModels.LastActivities;

    public class LastActivitiesController : BaseController
    {
        private const int PostShortTitleLenght = 25;

        public LastActivitiesController(IForumSystemData data)
            : base(data)
        {
        }

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

            var model = new PostLastActivityViewModel();

            if (lastAnswer == null && lastComment != null)
            {
                model.Comment = Mapper.Map<CommentConciseViewModel>(lastComment);

                return this.PartialView("_PostLastActivityPartial", model);
            }

            if (lastComment == null && lastAnswer != null)
            {
                model.Answer = Mapper.Map<AnswerConciseViewModel>(lastAnswer);

                return this.PartialView("_PostLastActivityPartial", model);
            }

            if (lastAnswer != null && lastComment != null)
            {
                var lastDate = this.GetLastDate(lastAnswer.CreatedOn, lastComment.CreatedOn);
                if (DateTime.Compare(lastDate.GetValueOrDefault(), lastComment.CreatedOn) == 0)
                {
                    model.Comment = Mapper.Map<CommentConciseViewModel>(lastComment);

                    return this.PartialView("_PostLastActivityPartial", model);
                }

                model.Answer = Mapper.Map<AnswerConciseViewModel>(lastAnswer);
            }

            return this.PartialView("_PostLastActivityPartial", model);
        }

        private DateTime? GetLastDate(DateTime? firstDate, DateTime? secondDate)
        {
            if (!firstDate.HasValue)
            {
                return secondDate;
            }

            if (!secondDate.HasValue)
            {
                return firstDate;
            }

            var checkDate = DateTime.Compare(firstDate.GetValueOrDefault(), secondDate.GetValueOrDefault());

            return checkDate < 0 ? secondDate : firstDate;
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

            var model = new CategoryAllRepliesViewModel { AllReplies = allReplies };

            return this.PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult LastPost(int? id)
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

            var lastPost =
                this.Data.Posts.All()
                    .Where(p => p.CategoryId == id)
                    .OrderByDescending(p => p.CreatedOn)
                    .FirstOrDefault();

            if (lastPost == null)
            {
                return new EmptyResult();
            }

            string shortTitle = this.SubstringTitle(lastPost.Title, PostShortTitleLenght);

            var model = new CategoryLastPostViewModel
                            {
                                Id = lastPost.Id, 
                                AuthorId = lastPost.AuthorId, 
                                Author = lastPost.Author.UserName, 
                                CreatedOn = lastPost.CreatedOn, 
                                Title = shortTitle
                            };

            return this.PartialView("_CategoryLastPostPartial", model);
        }

        private string SubstringTitle(string title, int maxLenght)
        {
            StringBuilder newTitle = new StringBuilder();

            newTitle.AppendFormat(
                title.Length < maxLenght ? $"{title.Substring(0, title.Length)}" : $"{title.Substring(0, maxLenght)}...");

            return newTitle.ToString();
        }
    }
}