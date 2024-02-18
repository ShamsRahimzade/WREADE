using System.ComponentModel.DataAnnotations.Schema;
using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Book:BaseNameableEntity
    {
        public List<Chapter>? Chapters { get; set; }
        
        public string Description { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }
        public string CoverPhoto { get; set; }
        public ICollection<BookTag >? BookTags { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
        public List<LibraryItem> Libraries { get; set; }
        public double Rating { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAdult { get; set; }
      
    }
}
