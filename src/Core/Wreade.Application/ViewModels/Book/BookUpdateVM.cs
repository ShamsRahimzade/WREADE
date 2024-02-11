using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
    public class BookUpdateVM
    {
        public string Name { get; set; }
        public int Part { get; set; }
        public string Description { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<BookTag>? BookTags { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public double Rating { get; set; }
        public bool IsCompleted { get; set; }
    }
}
