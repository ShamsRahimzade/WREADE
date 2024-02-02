using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}
