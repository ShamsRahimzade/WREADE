using Wreade.Domain.Entities.Common;

namespace WEB.Models
{
    public class Tag:BaseNameableEntity
    {
        public List<BookTag> BookTags { get; set; }
    }
}
