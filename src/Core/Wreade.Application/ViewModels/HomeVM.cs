using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
    public class HomeVM
    {
        public ICollection<Book> Books { get; set; }
		public string UserName { get; set; }
        public List<AppUser> Users { get; set; }
        public ICollection<Category> MostReadCategory { get; set; }
		public ICollection<Book> ReadingHistory { get; set; }
		
	}
}
