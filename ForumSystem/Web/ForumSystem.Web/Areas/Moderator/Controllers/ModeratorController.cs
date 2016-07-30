namespace ForumSystem.Web.Areas.Moderator.Controllers
{
    using System.Web.Mvc;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers;

    [Authorize(Roles = RoleConstants.Moderator)]
    public class ModeratorController : BaseController
    {
        protected ModeratorController(IForumSystemData data)
            : base(data)
        {
        }

        protected ModeratorController(IForumSystemData data, ApplicationUser userProfile)
            : base(data, userProfile)
        {
        }
    }
}