namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Areas.Administration.Controllers.Base;
    using ForumSystem.Web.Areas.Administration.InputModels.Users;
    using ForumSystem.Web.Areas.Administration.ViewModels.Users;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

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
                    .ProjectTo<UserViewModel>()
                    .ToList();

            return this.View(users);
        }

        [HttpGet]
        public ActionResult AddUserInRole(string id)
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

            var identityStore = new IdentityDbContext();
            var roles = identityStore.Roles.Select(r => r.Name).ToList();
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var availableUserRoles = new List<SelectListItem>();

            foreach (var role in roles)
            {
                if (!userManager.IsInRole(user.Id, role))
                {
                    availableUserRoles.Add(new SelectListItem { Value = role, Text = role });
                }
            }

            var model = new AddUserInRoleInputModel
                            {
                                UserName = user.UserName, 
                                UserId = user.Id, 
                                Roles = availableUserRoles
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserInRole(AddUserInRoleInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                userManager.AddToRole(input.UserId, input.RoleName);

                this.Data.SaveChanges();

                return this.RedirectToAction("All", "Users", new { area = "Administration" });
            }

            return this.View(input);
        }

        [HttpGet]
        public ActionResult RemoveUserFromRole(string id)
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

            var identityStore = new IdentityDbContext();
            var roles = identityStore.Roles.Select(r => r.Name).ToList();
            var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var availableUserRoles = new List<SelectListItem>();

            foreach (var role in roles)
            {
                if (userManager.IsInRole(user.Id, role))
                {
                    availableUserRoles.Add(new SelectListItem { Value = role, Text = role });
                }
            }

            var model = new AddUserInRoleInputModel
                            {
                                UserName = user.UserName, 
                                UserId = user.Id, 
                                Roles = availableUserRoles
                            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserFromRole(AddUserInRoleInputModel input)
        {
            if (input != null && this.ModelState.IsValid)
            {
                var userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                userManager.RemoveFromRole(input.UserId, input.RoleName);

                this.Data.SaveChanges();

                return this.RedirectToAction("All", "Users", new { area = "Administration" });
            }

            return this.View(input);
        }
    }
}