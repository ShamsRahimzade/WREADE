using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public  class BookCreateVM
	{
        public string Name { get; set; }
        public int Part { get; set; }
        public string Description { get; set; }
        public int AppUserId { get; set; }
        public AppUser User { get; set; }
        public List<Image> Images { get; set; }
        public List<BookTag> BookTags { get; set; }
        public List<BookCategory> BookCategories { get; set; }
        public double Rating { get; set; }
        public bool IsCompleted { get; set; }
    }
}
