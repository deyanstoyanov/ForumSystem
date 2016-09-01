namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.PostUpdates;

    public class PostUpdatesController : BaseController
    {
        public PostUpdatesController(IForumSystemData data)
            : base(data)
        {
        }

        [ChildActionOnly]
        public ActionResult GetById(int? id)
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

            var model =
                this.Data.PostUpdates.All()
                    .Where(u => u.PostId == id)
                    .OrderByDescending(u => u.CreatedOn)
                    .ProjectTo<PostUpdateViewModel>()
                    .FirstOrDefault();

            if (model == null)
            {
                return new EmptyResult();
            }

            return this.PartialView(model);
        }
    }
}