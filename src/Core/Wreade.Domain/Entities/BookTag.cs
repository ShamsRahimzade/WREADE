using Wreade.Domain.Entities.Common;

namespace Wreade.Domain.Entities
{
    public class BookTag:BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
