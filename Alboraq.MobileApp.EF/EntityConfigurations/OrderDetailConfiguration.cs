using Alboraq.MobileApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.EntityConfigurations
{
    internal class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetails");
            HasKey(x => x.ID);

            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ProductNo).HasMaxLength(20);            
        }
    }
}
