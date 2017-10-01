using Alboraq.MobileApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.EntityConfigurations
{
    internal class AppointmentConfiguration : EntityTypeConfiguration<Appointment>
    {
        public AppointmentConfiguration()
        {
            ToTable("Appointments");
            HasKey(x => x.ID);

            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.CustomerNo).HasMaxLength(128);
            Property(x => x.PlateNo).HasMaxLength(30);
            Property(x => x.MobileNo).HasMaxLength(20);
            
        }
    }
}
