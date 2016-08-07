namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Infrastructure.Extensions;
    using ForumSystem.Web.InputModels.Comments;
    using ForumSystem.Web.ViewModels.Comments;

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

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return this.PartialView("_CommentDetailPartial", viewModel);
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
            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CommentEditModel { Id = comment.Id, Content = comment.Content };

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] CommentEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var comment = this.Data.Comments.GetById(model.Id);

                if (comment.AuthorId != userId && !this.User.IsModerator() && !this.User.IsAdmin())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                comment.Content = model.Content;

                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();

                var viewModel = Mapper.Map<CommentViewModel>(comment);

                return this.PartialView("_CommentDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }
    }
}