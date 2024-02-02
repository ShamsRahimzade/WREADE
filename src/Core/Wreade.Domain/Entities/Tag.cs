using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class Tag:BaseNameableEntity
    {
        public List<BookTag> BookTags { get; set; }
    }
}
