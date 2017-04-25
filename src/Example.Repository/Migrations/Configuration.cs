namespace Example.Repository.Migrations
{
    using Domain.Entities.Authorization;
    using Ls.Utilities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Example.Repository.ExampleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Example.Repository.ExampleDbContext context)
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
            var tenantId = 1;
            context.Users.AddOrUpdate(p => p.Name,
                new User
                {
                    Id = LsIdGenerator.CreateIdentity(),
                    Role = new Role
                    {
                        Id = LsIdGenerator.CreateIdentity(),
                        Name = "管理员",
                        Description = "管理员",
                        TenantId = tenantId
                    },
                    LoginId = "admin",
                    Name = "admin",
                    Password = "123456",
                    Phone = "18795950000",
                    Email = "Charlesshang@outlook.com",
                    RealName = "管理员",
                    TenantId = tenantId,
                    CreatedTime = DateTime.Now,
                    LastUpdatedTime = DateTime.Now,
                });
        }
    }
}
