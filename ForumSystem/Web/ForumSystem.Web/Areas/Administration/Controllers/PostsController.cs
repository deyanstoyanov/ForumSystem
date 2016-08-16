namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.ViewModels.Posts;

    public class PostsController : AdministrationController
    {
        public PostsController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var posts = this.Data.Posts.AllWithDeleted().OrderByDescending(p => p.CreatedOn).ProjectTo<PostViewModel>();

            return this.View(posts);
        }
    }
}