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
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(a => a.Title).IsRequired()
                                         .HasMaxLength(200);

            builder.Property(a => a.Description).IsRequired()
                                                .HasColumnType("ntext")
                                                 .HasMaxLength(300); ;

            builder.Property(a => a.CreatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
