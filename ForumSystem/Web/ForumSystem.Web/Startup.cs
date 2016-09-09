using ForumSystem.Web;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace ForumSystem.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}