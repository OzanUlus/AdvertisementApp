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
    public class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            builder.Property(ps => ps.Description).HasColumnType("ntext")
                                                  .IsRequired()
                                                  .HasMaxLength(200);

            builder.Property(ps => ps.ImagePath).HasMaxLength(500)
                                                .IsRequired();

            builder.Property(ps => ps.Title).HasMaxLength(100)
                                            .IsRequired();

            builder.Property(ps => ps.CreatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
