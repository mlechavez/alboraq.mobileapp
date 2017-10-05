using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.EF.EntityConfigurations;
using Alboraq.MobileApp.EF.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Alboraq.MobileApp.EF
{
    public class ApplicationUser : IdentityUser
    {
        public string PlateNo { get; set; }
        public string Name { get; set; }        
    }

    public class AlboraqAppContext : IdentityDbContext<ApplicationUser>
    {
        public AlboraqAppContext()
            :base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AlboraqAppContext, Configuration>());
        }

        public AlboraqAppContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Configurations.Add(new AppointmentConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
                        
        }
    }    
}
