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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(au => au.FirstName).IsRequired()
                                               .HasMaxLength(50);

            builder.Property(au => au.Surname).IsRequired()
                                              .HasMaxLength(50);

            builder.Property(au => au.Username).IsRequired()
                                               .HasMaxLength(50);

            builder.Property(au => au.PhoneNumber).HasMaxLength(11)
                                                  .IsRequired();

            builder.Property(au => au.Password).IsRequired()
                                               .HasMaxLength(20);

            builder.HasOne(au => au.Gender).WithMany(au => au.AppUsers)
                   .HasForeignKey(au => au.GenderId);
        }
    }
}
