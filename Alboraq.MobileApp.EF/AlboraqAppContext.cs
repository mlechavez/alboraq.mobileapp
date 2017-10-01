using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.EF.EntityConfigurations;
using System.Data.Entity;

namespace Alboraq.MobileApp.EF
{
    public class AlboraqAppContext : DbContext
    {
        public AlboraqAppContext()
            :base("DefaultConnection")
        {
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
            modelBuilder.Configurations.Add(new AppointmentConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
