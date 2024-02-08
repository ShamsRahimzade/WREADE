using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Book:BaseNameableEntity
    {
        public int Part { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }
        public List<Image> Images { get; set; }
        public List<BookTag > BookTags { get; set; }
        public List<BookCategory>? BookCategories { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public double Rating { get; set; }
        public bool IsCompleted { get; set; }
    }
}
