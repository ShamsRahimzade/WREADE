

using Wreade.Domain.Entities.Common;

namespace WEB.Models
{
    public class Author:BaseEntity
    {
        public string SelfInformation { get; set; }
        public List<Image>? Images { get; set; }
        public string? Location { get; set; }
        public string? Instagram { get; set; }
    }
}
