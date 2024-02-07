using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        public string Image { get; set; }
        public int Rating { get; set; }
        public List<BookCategory>? BookCategories { get; set; }
    }
}
