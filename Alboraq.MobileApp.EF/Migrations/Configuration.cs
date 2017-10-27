namespace Alboraq.MobileApp.EF.Migrations
{
    using Alboraq.MobileApp.Core.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AlboraqAppContext>
    {
        public Configuration()
        {            
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AlboraqAppContext context)
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
            
            context.Roles.AddOrUpdate(r => r.Name,                
                new IdentityRole { Name = "receptionist" },
                new IdentityRole { Name = "parts salesman" },
                new IdentityRole { Name = "crm" }
            );            
        }
    }
}
