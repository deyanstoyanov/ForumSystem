namespace ForumSystem.Web.Areas.Moderator.Controllers.Base
{
    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Controllers.Base;
    using ForumSystem.Web.Infrastructure.Attributes;

    [AuthorizeRoles(RoleConstants.Moderator, RoleConstants.Administrator)]
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