namespace ForumSystem.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.UnitOfWork;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new ForumSystemDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<ForumSystemData>()
                .As<IForumSystemData>()
                .InstancePerRequest();
            builder.RegisterType<UserStore<ApplicationUser>>()
                .As<IUserStore<ApplicationUser>>()
                .InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>()
                .AsSelf()
                .InstancePerRequest();

            builder.Register(x => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>()
                .InstancePerRequest();
        }
    }
}