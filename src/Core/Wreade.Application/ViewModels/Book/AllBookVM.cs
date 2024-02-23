using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class AllBookVM
	{
		public List<Book> Books { get; set; }
		public List<Category> Categories { get; set; }
		
		public int ChapterCountFrom { get; set; }
		public int ChapterCountTo { get; set; }
	}
}
