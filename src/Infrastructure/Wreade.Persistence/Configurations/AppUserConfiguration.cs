using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace Wreade.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name)
                 .IsRequired()
                 .HasMaxLength(50);
            builder.Property(x => x.SurName)
                 .IsRequired()
                 .HasMaxLength(50);
        }
    }
}
