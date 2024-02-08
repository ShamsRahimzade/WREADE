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
		public ICollection<BookCategory> MostReadCategory { get; set; }
		public List<Book> ReadingHistory { get; set; }
		
    }
}
