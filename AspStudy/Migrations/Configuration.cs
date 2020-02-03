namespace AspStudy.Migrations
{
    using AspStudy.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspStudy.Data.AspStudyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AspStudy.Data.AspStudyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var adminUser = new User()
            {
                id = 1,
                UserName = "admin",
                Password = "pass",
                roles = new List<Role>()
            };

            var hiroshi = new User()
            {
                id = 2,
                UserName = "hiroshi",
                Password = "pass",
                roles = new List<Role>()
            };

            var admin = new Role()
            {
                Id = 1,
                RoleName = "admin",
                users = new List<User>()
            };

            var user = new Role()
            {
                Id = 2,
                RoleName = "User",
                users = new List<User>()
            };

            adminUser.roles.Add(admin);
            admin.users.Add(adminUser);
            hiroshi.roles.Add(user);
            user.users.Add(hiroshi);

            context.Users.AddOrUpdate(u => u.id, new User[] { adminUser, hiroshi });
            context.Roles.AddOrUpdate(r => r.Id, new Role[] { admin, user });
        }
    }
}
