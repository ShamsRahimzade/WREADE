using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
	public class Like:BaseEntity
	{
		public string LikerId { get; set; }
		public int ChapterId { get; set; }
		public AppUser Liker { get; set; }
		public Chapter Chapter { get; set; }
	}
}
