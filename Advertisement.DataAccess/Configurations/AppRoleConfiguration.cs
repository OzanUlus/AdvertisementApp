using AdvertisementApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(ar => ar.Definition).IsRequired()
                                                 .HasMaxLength(300);
            //builder.HasData(new AppRole[]
            //{
            //  new()
            //  {
            //    Definition = "admin",
            //    Id = 1,

            //  },
            //  new()
            //  {
            //    Definition = "member",
            //    Id = 2,
            //  }

            //});
        }
    }
}
