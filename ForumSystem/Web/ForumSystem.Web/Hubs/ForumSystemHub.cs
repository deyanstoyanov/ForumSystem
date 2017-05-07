namespace ForumSystem.Web.Hubs
{
    using System.Collections.Generic;

    using ForumSystem.Data;
    using ForumSystem.Data.UnitOfWork;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [Authorize]
    [HubName("forumSystemHub")]
    public class ForumSystemHub : Hub
    {
        public static IForumSystemData Data => new ForumSystemData(new ForumSystemDbContext());
    }
}
