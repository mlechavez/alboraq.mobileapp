using Alboraq.MobileApp.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Alboraq.MobileApp.EF.EntityConfigurations
{
    internal class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            ToTable("ProductCategories");
            HasKey(x => x.ProductCategoryID);

            Property(x => x.ProductCategoryID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).IsRequired();

            HasMany(x => x.Products).WithOptional(x => x.ProductCategory).HasForeignKey(x => x.ProductCategoryID);
        }
    }
}
