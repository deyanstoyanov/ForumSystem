namespace ForumSystem.Web.Areas.Administration.InputModels.Users
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AddUserInRoleInputModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}