namespace ForumSystem.Web.Areas.Moderator
{
    using System.Web.Mvc;

    public class ModeratorAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Moderator";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Moderator_default", 
                "Moderator/{controller}/{action}/{id}", 
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}