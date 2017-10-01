using Alboraq.MobileApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.EntityConfigurations
{
    internal class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");
            HasKey(x => x.OrderNo);

            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.OrderNo).HasMaxLength(7);
            Property(x => x.OrderDate).IsOptional();
            Property(x => x.CustomerNo).HasMaxLength(128);

            HasMany(x => x.OrderDetails)
                .WithRequired(x => x.Order)
                .HasForeignKey(x => x.OrderNo);
        }
    }
}
