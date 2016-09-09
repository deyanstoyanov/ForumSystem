namespace ForumSystem.Web.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;
    using ForumSystem.Web.Hubs;
    using ForumSystem.Web.Infrastructure.ActionResults;
    using ForumSystem.Web.Infrastructure.Extensions;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    public class BaseController : Controller
    {
        protected BaseController(IForumSystemData data)
        {
            this.Data = data;
        }

        protected BaseController(IForumSystemData data, ApplicationUser userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        public IForumSystemData Data { get; }

        public ApplicationUser UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(
            RequestContext requestContext, 
            AsyncCallback callback, 
            object state)
        {
            if (requestContext.HttpContext.User.IsLoggedIn())
            {
                var username = requestContext.HttpContext.User.Identity.GetUserName();
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
                this.ViewBag.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult<T> JsonSuccess<T>(T data)
        {
            return new StandardJsonResult<T> { Data = data };
        }

        protected void UpdateNotificationsCount(ApplicationUser author)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ForumSystemHub>();
            var notificationsCount = this.Data.Notifications.All().Count(n => n.ReceiverId == author.Id && !n.IsChecked);
            context.Clients.User(author.UserName).updateNotificationsCount(notificationsCount);
        }
    }
}