using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class BookDetailVM
	{
        public List<Book> Books { get; set; }
        public Book book { get; set; }
    }
}
