using Alboraq.MobileApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.EntityConfigurations
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");
            HasKey(x => x.ProductNo);

            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.ProductNo).HasMaxLength(20);
            Property(x => x.ProductName).HasMaxLength(150);
            Property(x => x.ProductDescription).IsMaxLength();

            HasMany(x => x.OrderDetails)
                .WithRequired(x => x.Product)
                .HasForeignKey(x => x.ProductNo);  

        }
    }
}