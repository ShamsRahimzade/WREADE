using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class BookItemVM
	{
        public int BookId { get; set; }
        public List<Chapter>? Chapters { get; set; }

		public string Description { get; set; }
		public string? AppUserId { get; set; }
		public AppUser? User { get; set; }
		public string CoverPhoto { get; set; }
		public ICollection<BookTag>? BookTags { get; set; }
		public ICollection<BookCategory>? BookCategories { get; set; }
		public ICollection<Comment>? Comments { get; set; }
		public double Rating { get; set; }
		public bool IsCompleted { get; set; }
		public bool IsAdult { get; set; }
	}
}
