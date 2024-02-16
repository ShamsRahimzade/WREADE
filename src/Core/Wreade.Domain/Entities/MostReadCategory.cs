using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Domain.Entities
{
	public class MostReadCategory
	{
		public Category Category { get; set; }
		public List<Book> Books { get; set; }
	}
}
