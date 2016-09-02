namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Moderator.Controllers.Base;
    using ForumSystem.Web.Areas.Moderator.InputModels.Comments;
    using ForumSystem.Web.ViewModels.Comments;

    using Microsoft.AspNet.Identity;

    public class CommentsController : ModeratorController
    {
        public CommentsController(IForumSystemData data)
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

            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = new CommentEditModel { Id = comment.Id, Content = comment.Content };

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var comment = this.Data.Comments.GetById(model.Id);

                comment.Content = model.Content;

                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();

                if (model.Reason != null)
                {
                    var commentUpdate = new CommentUpdate
                                            {
                                                AuthorId = userId, 
                                                CommentId = comment.Id, 
                                                Reason = model.Reason
                                            };

                    this.Data.CommentUpdates.Add(commentUpdate);
                    this.Data.SaveChanges();
                }

                var viewModel = Mapper.Map<CommentViewModel>(comment);
                viewModel.IsUpdating = true;

                return this.PartialView("_CommentDetailPartial", viewModel);
            }

            return this.JsonError("Content is required");
        }
    }
}