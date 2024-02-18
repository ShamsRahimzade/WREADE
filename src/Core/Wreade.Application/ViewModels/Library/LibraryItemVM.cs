using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class LibraryItemVM
	{
        public int Id { get; set; }
        public string? AppUserid { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
