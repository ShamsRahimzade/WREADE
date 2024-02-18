using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class SearchVM
	{
        public int? Order { get; set; }
        public List<Book> books { get; set; }
        public List<AppUser> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
