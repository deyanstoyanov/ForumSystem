namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.InputModels.Users;
    using ForumSystem.Web.ViewModels.Answers;
    using ForumSystem.Web.ViewModels.Comments;
    using ForumSystem.Web.ViewModels.Posts;
    using ForumSystem.Web.ViewModels.Users;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    [Authorize]
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

        [HttpGet]
        public ActionResult Edit(string id)
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

            var model = Mapper.Map<UserEditModel>(user);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel model)
        {
            var user = this.Data.Users.GetById(model.Id);
            var loggedUserId = this.User.Identity.GetUserId();
            if (user.Id != loggedUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var checkEmail = this.Data.Users.All().Any(u => u.Email == model.Email);
            var checkUserName = this.Data.Users.All().Any(u => u.UserName == model.UserName);

            if (checkEmail && user.Email != model.Email)
            {
                this.ModelState.AddModelError("Email", $"There is an existing account associated with {model.Email}");
            }

            if (checkUserName && user.UserName != model.UserName)
            {
                this.ModelState.AddModelError("UserName", "That username is taken. Try another.");
            }

            if (this.ModelState.IsValid)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.AboutMe = model.AboutMe;
                user.City = model.City;
                user.Country = model.Country;
                user.Interests = model.Interests;
                user.Occupation = model.Occupation;
                user.PictureUrl = model.PictureUrl;
                user.FacebookProfile = model.FacebookProfile;
                user.GitHubProfile = model.GitHubProfile;
                user.LinkedInProfile = model.LinkedInProfile;
                user.SkypeProfile = model.SkypeProfile;
                user.StackOverflowProfile = model.StackOverflowProfile;
                user.TwitterProfile = model.TwitterProfile;
                user.WebsiteUrl = model.WebsiteUrl;

                this.Data.Users.Update(user);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Users", new { area = string.Empty, id = user.Id });
            }

            return this.View(model);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Roles(string id)
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

            var roles = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(id);

            return this.PartialView("_UserRolesPartial", roles);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult GetUserImage(string id)
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

            var pictureUrl = this.Data.Users.All().Where(u => u.Id == id).Select(u => u.PictureUrl).FirstOrDefault();

            return this.PartialView("_UserImagePartial", pictureUrl);
        }
    }
}