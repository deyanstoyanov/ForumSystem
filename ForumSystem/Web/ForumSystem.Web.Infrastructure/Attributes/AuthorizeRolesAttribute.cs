namespace ForumSystem.Web.Infrastructure.Attributes
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using ForumSystem.Web.Infrastructure.Extensions;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] roles;

        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var user = httpContext.User;

            return user.IsLoggedIn() && this.roles.Any(x => user.IsInRole(x));
        }
    }
}