namespace ForumSystem.Web.Infrastructure.Extensions
{
    using System.Security.Principal;

    using ForumSystem.Common.Constants;

    public static class PrincipalExtensions
    {
        public static bool IsLoggedIn(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(RoleConstants.Administrator);
        }

        public static bool IsModerator(this IPrincipal principal)
        {
            return principal.IsInRole(RoleConstants.Moderator);
        }
    }
}