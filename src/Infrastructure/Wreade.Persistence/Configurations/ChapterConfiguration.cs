using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Configurations
{
	public class ChapterConfiguration: IEntityTypeConfiguration<Chapter>
	{
       public void Configure (EntityTypeBuilder<Chapter> builder)
		{
			builder.HasOne(c => c.Book)
				.WithMany()
				.HasForeignKey(c => c.BookId)
				.OnDelete(DeleteBehavior.Restrict);
		}
    }
}
