namespace ForumSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Common.Constants;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedRoles(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roleNames = new[] { RoleConstants.Administrator, RoleConstants.Moderator };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole { Name = roleName };
                    roleManager.Create(role);
                }

                context.SaveChanges();
            }
        }
    }
}