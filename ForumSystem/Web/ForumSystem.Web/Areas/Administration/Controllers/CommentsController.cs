namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.InputModels.Comments;
    using ForumSystem.Web.Areas.Administration.ViewModels.Comments;

    public class CommentsController : AdministrationController
    {
        public CommentsController(IForumSystemData data)
            : base(data)
        {
        }

        public ActionResult All()
        {
            var comments =
                this.Data.Comments.AllWithDeleted()
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>()
                    .ToList();

            return this.View(comments);
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

            var model = new CommentEditModel
                            {
                                Id = comment.Id, 
                                Content = comment.Content, 
                                IsDeleted = comment.IsDeleted
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var comment = this.Data.Comments.GetById(model.Id);

                comment.Content = model.Content;
                comment.IsDeleted = model.IsDeleted;

                this.Data.Comments.Update(comment);
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

            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<CommentViewModel>(comment);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var comment = this.Data.Comments.GetById(id);
            if (comment == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Comments.Delete(id);
            this.Data.SaveChanges();

            return this.RedirectToAction("All");
        }
    }
}