using Microsoft.Owin;

[assembly: OwinStartup(typeof(ForumSystem.Web.Startup))]
namespace ForumSystem.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
