namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.InputModels.Comments;
    using ForumSystem.Web.ViewModels.Comment;

    using Microsoft.AspNet.Identity;

    public class CommentsController : BaseController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult All(int? id)
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

            var comments =
                this.Data.Comments.All()
                    .Where(c => c.AnswerId == id)
                    .OrderBy(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>();

            return this.PartialView(comments);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? id)
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

            var inputModel = new CommentInputModel { AnswerId = answer.Id };

            return this.PartialView(inputModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnswerId,Content")] CommentInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var comment = new Comment
                                  {
                                      AnswerId = input.AnswerId,
                                      Content = input.Content,
                                      AuthorId = userId
                                  };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var model = new CommentViewModel
                                {
                                    Id = comment.Id, 
                                    AuthorId = comment.AuthorId, 
                                    Author = comment.Author.UserName, 
                                    AuthorPictureUrl = comment.Author.PictureUrl, 
                                    AnswerId = comment.AnswerId, 
                                    Content = comment.Content, 
                                    CreatedOn = comment.CreatedOn
                                };

                return this.PartialView("_CommentDetailPartial", model);
            }

            return this.JsonError("Content is required");
        }
    }
}