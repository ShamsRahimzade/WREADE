using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Tag:BaseNameableEntity
    {
        public ICollection<BookTag>? BookTags { get; set; }
        public int Rating { get; set; }
    }
}
