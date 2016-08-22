namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.ViewModels.Answers;
    using ForumSystem.Web.ViewModels.Comments;
    using ForumSystem.Web.ViewModels.Posts;
    using ForumSystem.Web.ViewModels.Users;

    public class UsersController : BaseController
    {
        public UsersController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.GetById(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var model = Mapper.Map<UserViewModel>(user);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Posts(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.GetById(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var posts =
                this.Data.Posts.All()
                    .Where(p => p.AuthorId == id)
                    .OrderByDescending(p => p.CreatedOn)
                    .ProjectTo<PostViewModel>()
                    .ToList();

            return this.PartialView("_UserPostsPartial", posts);
        }

        [HttpGet]
        public ActionResult Answers(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.GetById(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var answers =
                this.Data.Answers.All()
                    .Where(a => a.AuthorId == id)
                    .OrderByDescending(a => a.CreatedOn)
                    .ProjectTo<AnswerViewModel>()
                    .ToList();

            return this.PartialView("_UserAnswersPartial", answers);
        }

        [HttpGet]
        public ActionResult Comments(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.GetById(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var comments =
                this.Data.Comments.All()
                    .Where(c => c.AuthorId == id)
                    .OrderByDescending(c => c.CreatedOn)
                    .ProjectTo<CommentViewModel>()
                    .ToList();

            return this.PartialView("_UserCommentsPartial", comments);
        }
    }
}