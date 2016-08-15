namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.ViewModels.Users;

    public class UsersController : AdministrationController
    {
        public UsersController(IForumSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult All()
        {
            var users =
                this.Data.Users.AllWithDeleted()
                    .OrderByDescending(u => u.CreatedOn)
                    .ProjectTo<UserViewModel>();

            return this.View(users);
        }
    }
}