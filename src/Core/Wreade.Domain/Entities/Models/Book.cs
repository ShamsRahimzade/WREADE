using Wreade.Domain.Entities.Common;

namespace WEB.Models
{
    public class Book:BaseNameableEntity
    {
        public int Part { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public List<BookTag > BookTags { get; set; }
        public List<BookCategory > BookCategories { get; set; }

    }
}
