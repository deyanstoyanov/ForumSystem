namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Common.Constants;
    using ForumSystem.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ForumSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ForumSystemDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedRoles(ForumSystemDbContext context)
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

        private void SeedUsers(ForumSystemDbContext context)
        {
            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser
                                {
                                    UserName = "administrator", 
                                    Email = "administrator@fake.com", 
                                    CreatedOn = DateTime.Now
                                };

                var userCreateResult = userManager.Create(admin, "123456");
                if (userCreateResult.Succeeded)
                {
                    context.Users.AddOrUpdate(admin);
                }

                context.SaveChanges();

                userManager.AddToRole(admin.Id, RoleConstants.Administrator);

                context.SaveChanges();
            }
        }
    }
}